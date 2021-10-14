<template>
  <v-app>
    <v-app-bar
      app
      color="primary"
      dark
    >
      <div class="d-flex align-center">
        <v-img
          alt="Vuetify Logo"
          class="shrink mr-2"
          contain
          :src="require('../src/assets/logo-white.svg')"
          transition="scale-transition"
          width="80"
        />

        <p class="text text-h4 mb-0">ShopApiVue</p>
      </div>

      <v-tabs
          align-with-title
      >
        <v-tabs-slider color="yellow"></v-tabs-slider>

        <v-tab to="/home">
          Home
        </v-tab>
        <v-tab to="/brand">
          Brands
        </v-tab>
        <v-tab to="/shoe">
          Shoes
        </v-tab>
        <v-tab to="/User/login" v-if="!$store.getters.ISAUTH">
          Log In
        </v-tab>
        <v-tab to="/User/signup" v-if="!$store.getters.ISAUTH">
          Sing in
        </v-tab>
        <v-tab to="/User/Index" v-if="$store.getters.ISAUTH">
          User
        </v-tab>
        <v-btn @click="isAut1h()">Is Auth</v-btn>
      </v-tabs>
    </v-app-bar>

    <v-main>
      <router-view/>
    </v-main>
  </v-app>
</template>

<script>

import axios from "axios";
import {utils} from "./utils/utils";

export default {
  name: 'App',
  data: () => ({
    //
  }),
  methods:{
    isAut1h(){
      axios.get(utils.API.USER+"IsAuth").then((response)=>{
        console.log("asdasfas");
        console.log(response);
      })
    }
  }
};
</script>
