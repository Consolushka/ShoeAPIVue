import Vue from 'vue';
import Vuex from 'vuex';
import {User} from "../utils/classes";

Vue.use(Vuex);

export const store = new Vuex.Store({
    state: {
        IsAuth: false,
        user: new User()
    },
    getters: {
        ISAUTH: state => {
            return state.IsAuth;
        },
        GET_USER: state =>{
            return state.user;
        }
    },
    mutations: {
        LOGIN: (state, user) => {
            state.IsAuth = true;
            console.log(user);
            state.user = user;
        },
        LOGOUT: (state) => {
            state.IsAuth = false
        }
    },
    actions: {
        LOGIN: (context, user) => {
            context.commit('LOGIN', user);
        },
        LOGOUT: (context, user) => {
            user.LogOut();
            context.commit('LOGOUT');
        }
    },
});