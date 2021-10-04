const routes = [
    {path: '/home', component: require('./js/pages/home-page.vue').default},
    {path: '/brand', component: require('./js/pages/brand-page.vue').default},
    {path: '/shoe', component: require('./js/pages/shoe-page.vue').default}
];

import Vue from 'vue'
import VueRouter from 'vue-router'
import vuetify from '../src/plugins/vuetify.js' // path to vuetify export

Vue.component("brand", require("./js/components/brand.vue").default)
Vue.component("shoe", require("./js/components/shoe.vue").default)

Vue.use(VueRouter);

const router = new VueRouter({
   routes
});

const app = new Vue({
    el: "#app",
  router,
  vuetify,
  data(){
      return {
        variables: require("./js/utils.js").default
      }
  }
});
