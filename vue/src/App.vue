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

        <p class="text text-h4 mb-0">ShoeApiVue</p>
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
        <v-tab to="/User/login" v-if="$store.getters.ISAUTH !== 'true'">
          Log In
        </v-tab>
        <v-tab to="/User/signup" v-if="$store.getters.ISAUTH !== 'true'">
          Sing in
        </v-tab>
        <v-btn color="red" style="margin: auto 0" @click="$store.dispatch('LOGOUT')" v-if="$store.getters.ISAUTH === 'true'">
          Log Out
        </v-btn>
      </v-tabs>
    </v-app-bar>

    <v-main>
      <router-view/>
    </v-main>
    <alert v-if="snackBar" :snackbar="snackBar" :status="responseFine" @close="snackBar=false" :text="text"></alert>
  </v-app>
</template>

<script>

import Alert from "./components/alert";
import {eventBus} from "./main";
export default {
  name: 'App',
  components: {Alert},
  data: () => ({
    snackBar: false,
    responseFine: true,
    text: ""
  }),
  mounted() {
    if(localStorage.getItem("userId") !== '0' && localStorage.getItem("userId") !== null){
      this.$store.commit('LOGIN', localStorage.getItem("userId"));
    }
  },
  created() {
    eventBus.$on('showNotification', (params)=>{
      console.log(params);
      this.snackBar = params.snackBar;
      this.responseFine = params.responseFine;
      this.text = params.text;
    })
  }
};
</script>
