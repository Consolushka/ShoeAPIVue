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
        LOGIN: (state) => {
            state.IsAuth = true;
        },
        LOGOUT: (state) => {
            state.IsAuth = false
        }
    },
    actions: {
        LOGIN: (context) => {
            context.commit('LOGIN');
        },
        LOGOUT: (context, user) => {
            user.LogOut();
            context.commit('LOGOUT');
        }
    },
});