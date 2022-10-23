/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{razor,html,cshtml,js}",
    "./LinkBook/Views/*.{razor,html,cshtml,js}",
    "./LinkBook/Views/**/*.{razor,html,cshtml,js}",
    "./LinkBook/Views/Home/*.{razor,html,cshtml,js}",
    "./LinkBook/Views/Category/*.{razor,html,cshtml,js}",
    "./LinkBook/Views/Shared/*.{razor,html,cshtml,js}",
    "./LinkBook/wwwroot/js/*.{razor,html,cshtml,js}"
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}