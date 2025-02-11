/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./**/*.{razor,html,cshtml}",
        "./node_modules/flowbite/**/*.js"
    ],
    theme: {
        screens: {
            sm: '480px',
            md: '768px',
            lg: '976px',
            xl: '1440px'
        },
        extend: {
            colors: {
                brightRed:  '#880808',
            },
        },
    },
    plugins: [
        require('flowbite/plugin')
    ],
}

