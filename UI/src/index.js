const routes = [
    {path: '/home', component: require('./js/pages/home-page.vue').default},
    {path: '/brand', component: require('./js/pages/brand-page.vue').default},
    {path: '/shoe', component: require('./js/pages/shoe-page.vue').default}
];

import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.component("brand", require("./js/components/brand.vue").default)

Vue.use(VueRouter);

const router = new VueRouter({
   routes
});

const app = new Vue({
    el: "#app",
  router,
  data(){
      return {
        variables: require("./js/utils.js").default
      }
  }
});
