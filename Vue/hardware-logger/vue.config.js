// vue.config.js
module.exports = {
  devServer: {
    proxy: {
      '^/api': {
        target: 'http://localhost:5000/',
        ws: true,
        changeOrigin: true
      },
      'http://localhost:5000/'
    },
    crossorigin=""
  }
}
