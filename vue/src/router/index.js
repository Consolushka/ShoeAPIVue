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
    path: '/User',
    name: 'User',
    component: () => import('../views/User.vue')
  },
  {
    path: '/Type',
    name: 'Type',
    component: () => import('../views/Types.vue')
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
  },
  {
    path: '/User/Confirm',
    name: 'confirm',
    component: () => import('../views/Confirmed.vue')
  }
]

const router = new VueRouter({
  routes
})

export default router
