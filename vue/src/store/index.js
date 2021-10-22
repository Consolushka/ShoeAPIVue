import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export const store = new Vuex.Store({
    state: {
        IsAuth: localStorage.getItem('IsAuth'),
        Token: localStorage.getItem('token')
    },
    getters: {
        ISAUTH: state => {
            return state.IsAuth;
        },
        TOKEN: state =>{
            return state.Token;
        },
        CONFIG_HEADER: state => {
            return {
                headers:{
                    "Authorization": state.Token
                }
            };
        }
    },
    mutations: {
        LOGIN: (state) => {
            state.IsAuth = 'true';
            state.Token = localStorage.getItem('token');
        },
        LOGOUT: (state) => {
            state.IsAuth = 'false';
            state.Token = localStorage.getItem('token');
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