const path = require('path');

module.exports = {
    entry: './wwwroot/index.js', // Your entry file
    output: {
        filename: 'bundle.js', // Output file name
        path: path.resolve(__dirname, 'wwwroot/dist'), // Output directory
    },
    module: {
        rules: [
            {
                test: /\.css$/, // Regex to match .css files
                use: ['style-loader', 'css-loader'], // Loaders to process CSS
            },
        ],
    },
    mode: 'development', // Change to 'production' for production build
    devServer: {
        static: path.join(__dirname, 'wwwroot'), // Serve content from wwwroot
        port: 3000, // Align with Docker EXPOSE
        hot: true,
        liveReload: true,
        server: 'http',
    },
};
