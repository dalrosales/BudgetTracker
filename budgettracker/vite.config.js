// https://vitejs.dev/config/
import { fileURLToPath, URL } from 'node:url';
import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';
import child_process from 'child_process';

const baseFolder =
    process.env.APPDATA !== undefined && process.env.APPDATA !== ''
        ? `${process.env.APPDATA}/ASP.NET/https`
        : `${process.env.HOME}/.aspnet/https`;

const certificateArg = process.argv.map(arg => arg.match(/--name=(?<value>.+)/i)).filter(Boolean)[0];
const certificateName = certificateArg ? certificateArg.groups.value : "BudgetTracker";

// Check if running in a CI environment (GitHub Actions, etc.)
const isCI = process.env.CI === 'true';

if (!certificateName && !isCI) {
    console.error('Invalid certificate name. Run this script in the context of an npm/yarn script or pass --name=<<app>> explicitly.')
    process.exit(-1);
}

const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

// Attempt to create the certificate (skip in CI environments)
try {
    if (!isCI && (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath))) {
        child_process.spawnSync('dotnet', [
            'dev-certs',
            'https',
            '--export-path',
            certFilePath,
            '--format',
            'Pem',
            '--no-password',
        ], { stdio: 'inherit' });
    }
} catch (error) {
    console.warn("Warning: Could not create certificate. HTTPS will not be available.");
}

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: {
        proxy: {
            '^/weatherforecast': {
                target: 'https://localhost:5001/',
                secure: false
            }
        },
        port: 5173,
        https: {
            key: fs.existsSync(keyFilePath) ? fs.readFileSync(keyFilePath) : undefined,
            cert: fs.existsSync(certFilePath) ? fs.readFileSync(certFilePath) : undefined,
        }
    },
    build: {
        outDir: 'dist', // Specify the output directory for build artifacts
    }
});
