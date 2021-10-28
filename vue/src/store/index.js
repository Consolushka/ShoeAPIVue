import Vue from 'vue';
import Vuex from 'vuex';
import {User} from "../utils/classes";
import axios from "axios";
import {utils} from "../utils/utils";

Vue.use(Vuex);

export const store = new Vuex.Store({
    state: {
        IsAuth: localStorage.getItem('IsAuth'),
        Token: localStorage.getItem('token'),
        User: new User()
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
        },
        IS_ADMIN: state => {
            return state.User.RoleId === 2;
        }
    },
    mutations: {
        LOGIN: (state, id) => {
            state.IsAuth= localStorage.getItem('IsAuth');
            state.Token = localStorage.getItem('token');
            let config = {
                headers:{
                    "Authorization": state.Token
                }
            };
            axios.post(utils.API.USER+`GetById?id=${id}`, null,config)
                .then((response)=>{
                    localStorage.setItem('userId', response.data.id);
                    state.User = new User(response.data);
                })
        },
        LOGOUT: (state) => {
            state.IsAuth = 'false';
            state.Token = localStorage.getItem('token');
            state.User = new User();
            localStorage.setItem('userId', "0");
        }
    },
    actions: {
        LOGIN: (context) => {
            context.commit('LOGIN');
        },
        LOGOUT: (context) => {
            context.commit('LOGOUT');
        }
    },
});