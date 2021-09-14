const routes = [
    {path: '/home', component: require('./js/components/home.vue').default},
    {path: '/brand', component: require('./js/components/brand.vue').default},
    {path: '/shoe', component: require('./js/components/shoe.vue').default}
];

import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.component("home", require("./js/components/home.vue").default)
Vue.component("brand", require("./js/components/brand.vue").default)
Vue.component("shoe", require("./js/components/shoe.vue").default)

Vue.use(VueRouter);

const router = new VueRouter({
   routes
});

const app = new Vue({
    el: "#app",
  router
});
