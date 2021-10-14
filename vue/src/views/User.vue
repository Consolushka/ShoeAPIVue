<template>
  <v-container>
    <p class="text text-h4 text-center">User Page</p>
    <v-btn color="red" @click="LogOut">LogOut</v-btn>
  </v-container>
</template>

<script>

import {User} from "../utils/classes";
import router from "../router";

export default {
  methods:{
    LogOut(){
      router.push('login');
      this.$store.dispatch('LOGOUT', new User());
    }
  },
  created() {
    console.log(new User().GET());
  }
}
</script>