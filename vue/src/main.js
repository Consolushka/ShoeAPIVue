import Vue from 'vue'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify'
import '@mdi/font/css/materialdesignicons.css' // Ensure you are using css-loader

Vue.config.productionTip = false

new Vue({
  router,
  vuetify,
  icons: {
    iconfont: 'mdi', // default - only for display purposes
  },
  render: h => h(App)
}).$mount('#app')
