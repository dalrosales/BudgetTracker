const path = require('path');

module.exports = {
    entry: './wwwroot/index.js',
    output: {
        filename: 'bundle.js',
        path: path.resolve(__dirname, 'wwwroot/dist'),
        publicPath: '/dist/', // Ensures assets are served from '/dist/' in production
    },
    module: {
        rules: [
            {
                test: /\.css$/,
                use: ['style-loader', 'css-loader'],
            },
        ],
    },
    mode: 'development',
    devServer: {
        static: {
            directory: path.join(__dirname, 'wwwroot'),
        },
        port: 3000, // Port for dev server (only serves assets)
        devMiddleware: {
            publicPath: '/dist/',
        },
        hot: true,
        liveReload: true,
    },
};
