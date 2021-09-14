const routes = [
    {path: '/home', component: home},
    {path: '/brand', component: brand},
    {path: '/home', component: shoe}
];

const router = new VueRouter({
   routes
});

const app = new Vue({
    router
}).$mount("#app");