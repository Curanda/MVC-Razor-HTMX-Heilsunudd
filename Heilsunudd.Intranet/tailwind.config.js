export default {
    content: [
        "./Views/**/*.cshtml",
        "./Areas/**/*.cshtml",
        "./wwwroot/**/*.{html,js}"
    ],
    plugins: [
        require('daisyui')
    ],
    daisyui: {
        themes: ["light", "dark"],
    }
}