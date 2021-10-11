import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [
  {
    path: '/home',
    name: 'Home',
    component: ()=> import('../views/Home.vue')
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  },
  {
    path: '/brand',
    name: 'brand',
    component: () => import('../views/Brands.vue')
  },
  {
    path: '/shoe',
    name: 'shoe',
    component: () => import('../views/Shoes.vue')
  },
  {
    path: '/User/login',
    name: 'login',
    component: () => import('../views/LogIn.vue')
  },
  {
    path: '/User/signup',
    name: 'signup',
    component: () => import('../views/SignUp.vue')
  }
]

const router = new VueRouter({
  routes
})

export default router
