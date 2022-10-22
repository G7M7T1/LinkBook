/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{cshtml,js}",
    "./LinkBook/Views/*.{cshtml,js}",
    "./LinkBook/Views/**/*.{cshtml,js}",
    "./LinkBook/Views/Home/*.{cshtml,js}",
    "./LinkBook/Views/Shared/*.{cshtml,js}",
    "./LinkBook/wwwroot/js/*.{cshtml,js}"
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}