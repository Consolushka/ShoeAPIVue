import Vue from "vue"
import Vuex from "vuex"

Vue.use(Vuex)

export default new Vuex.Auth({
   state: {
       isAuth: false
   },
    mutations:{
        // eslint-disable-next-line no-unused-vars
       Login(isAuth){
           isAuth = true
       },
        // eslint-disable-next-line no-unused-vars
        Logout(isAuth){
           isAuth = false
        }
    },
    getters:{
       GetAuth(){
           return this.state.isAuth
       }
    }
});