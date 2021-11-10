import Vue from 'vue';
import Vuex from 'vuex';
import {Brand, Shoe, User} from "../utils/classes";
import axios from "axios";
import {utils} from "../utils/utils";

Vue.use(Vuex);

export const store = new Vuex.Store({
    state: {
        IsAuth: localStorage.getItem('IsAuth'),
        Token: localStorage.getItem('token'),
        User: new User(),
        Brands: [],
        Shoes: []
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
        },
        BRANDS: state =>{
            return state.Brands;
        },
        SHOES: state=>{
            return state.Shoes;
        },
        USER: state=>{
            return state.User;
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
                    console.log(response.data);
                    state.User = new User(response.data);
                })
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
            axios.get(utils.API.BRANDS+"GetAll")
                .then((response)=>{
                    state.Brands = [];
                    response.data.forEach((brand)=>{
                        state.Brands.push(new Brand(brand));
                    })
                })
        },
        UPDATE_SHOES: (state)=>{
            axios.get(utils.API.SHOES+"GetAll")
                .then((response)=>{
                    state.Shoes = [];
                    response.data.forEach((shoe)=>{
                        state.Shoes.push(new Shoe(shoe));
                    })
                })
        }
    },
    actions: {
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
        }
    },
});