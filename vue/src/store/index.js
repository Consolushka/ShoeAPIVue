import Vue from 'vue';
import Vuex from 'vuex';
import {Brand, Shoe, Type, User} from "../utils/classes";
import axios from "axios";
import {utils} from "../utils/utils";

Vue.use(Vuex);

export const store = new Vuex.Store({
    state: {
        IsAuth: localStorage.getItem('IsAuth'),
        Token: localStorage.getItem('token'),
        User: new User(),
        Brands: [],
        Shoes: [],
        Types: []
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
            return state.User.IsAdmin;
        },
        BRANDS: state =>{
            return state.Brands;
        },
        TYPES: state =>{
            return state.Types;
        },
        SHOES: state=>{
            return state.Shoes;
        },
        USER: state=>{
            return state.User;
        }
    },
    mutations: {
        SET_TOKEN: (state)=>{
            console.log(localStorage.getItem('token'));
            state.Token = localStorage.getItem('token');
        },
        GET_USER: (state)=>{
            axios.get(utils.API.USER+`get-by-id?id=${localStorage.getItem('userId')}`, {headers:{"Authorization": state.Token}})
                .then((response)=>{
                    console.log(response.data);
                    state.User = new User(response.data);
                })
        },
        LOGIN: (state, object) => {
            state.IsAuth= localStorage.getItem('IsAuth');
            state.Token = localStorage.getItem('token');
            state.User = new User(object);
        },
        LOGOUT: (state) => {
            localStorage.setItem('IsAuth', "false");
            localStorage.setItem('token', "");
            localStorage.setItem('userId', "0");
            state.IsAuth = 'false';
            state.Token = localStorage.getItem('token');
            state.User = new User();
        },
        UPDATE_BRANDS: (state)=>{
            axios.get(utils.API.BRANDS+"get-all")
                .then((response)=>{
                    state.Brands = [];
                    response.data.forEach((brand)=>{
                        state.Brands.push(new Brand(brand));
                    })
                })
        },
        UPDATE_SHOES: (state)=>{
            axios.get(utils.API.GOODS+"get-all")
                .then((response)=>{
                    state.Shoes = [];
                    response.data.forEach((shoe)=>{
                        state.Shoes.push(new Shoe(shoe));
                    })
                })
        },
        UPDATE_TYPES: (state)=>{
            axios.get(utils.API.TYPES+"get-all")
                .then((response)=>{
                    state.Types = [];
                    response.data.forEach((type)=>{
                        state.Types.push(new Type(type));
                    })
                })
        }
    },
    actions: {
        SET_TOKEN: (context)=>{
          context.commit('SET_TOKEN');
        },
        GET_USER: (context)=>{
            context.commit('GET_USER');
        },
        LOGIN: (context) => {
            context.commit('LOGIN');
        },
        LOGOUT: (context) => {
            context.commit('LOGOUT');
        },
        UPDATE_BRANDS: (context)=>{
            context.commit('UPDATE_BRANDS');
        },
        UPDATE_SHOES: (context)=>{
            context.commit('UPDATE_SHOES');
        },
        UPDATE_TYPES: (context)=>{
            context.commit('UPDATE_TYPES');
        }
    },
});