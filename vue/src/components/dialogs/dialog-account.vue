<template>
  <v-form v-model="valid">
    <v-container>
      <v-row>
        <v-col
            cols="12"
        >
          <v-text-field
              v-model="user.Email"
              :rules="emailRules"
              label="E-mail"
              required
          ></v-text-field>
        </v-col>
        <v-col
            cols="12"
        >
          <v-text-field
              v-model="user.Password"
              label="Password"
              required
          ></v-text-field>
        </v-col>
        <v-col
            cols="12"
            v-if="!isLogin"
        >
          <v-text-field
              v-model="user.ConfirmPassword"
              label="Confirm Password"
              required
          ></v-text-field>
        </v-col>
        <p class="text text-h6 red--text" v-if="err!==null">{{ err }}</p>
      </v-row>
      <div></div>
      <v-btn @click="LogIn" v-if="isLogin">Log In</v-btn>
      <v-btn @click="SignUp" v-else>Sign Up</v-btn>
    </v-container>
  </v-form>
</template>

<script>

import {User} from "../../utils/classes";
import router from "../../router";

export default {
  name: "dialog-account",
  props: {
    isLogin: Boolean
  },
  data: () => ({
    valid: false,
    err: null,
    user: new User(),
    emailRules: [
      v => !!v || 'E-mail is required',
      v => /.+@.+/.test(v) || 'E-mail must be valid',
    ],
  }),
  methods: {
    LogIn() {
      this.user.Authenticate()
          .then(data => {
            console.log(data);
            localStorage.setItem('token', data.token);
            localStorage.setItem('IsAuth', "true");
            console.log(localStorage.getItem('IsAuth'));
            console.log(localStorage.getItem('token'));
            this.$store.commit('LOGIN', data.id);
            router.push({name: 'brand'});
          });
    },
    SignUp() {
      this.user.Register()
          .then(data => {
            localStorage.setItem('token', data.token);
            localStorage.setItem('isAuth', "true");
            this.$store.commit('LOGIN');
            router.push({name: 'brand'});
          });
    }
  }

}
</script>