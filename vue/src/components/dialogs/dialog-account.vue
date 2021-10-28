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
              type="password"
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
              type="password"
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
import {eventBus} from "../../main";

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
          .then(response => {
            if(response.status>=200 && response.status<=300){
              eventBus.$emit('showNotification', {responseFine: "Fine", snackBar: true, text: "Logged in"});
              localStorage.setItem('token', response.data.token);
              localStorage.setItem('IsAuth', "true");
              this.$store.commit('LOGIN', response.data.id);
              router.push({name: 'brand'});
            }
            else{
              eventBus.$emit('showNotification', {responseFine: "Error", snackBar: true, text: response.data});
            }
          });
    },
    SignUp() {
      this.user.Register()
          .then((res) => {
            if(res.status >=200 && res.status<=300){
              eventBus.$emit('showNotification', {responseFine: "Fine", snackBar: true, text: "Signed up successfully"})
              router.push({name: 'login'});
            }
            else{
              eventBus.$emit('showNotification', {responseFine: "Error", snackBar: true, text: res.data})
              console.log(res.data);
            }
          });
    }
  }

}
</script>