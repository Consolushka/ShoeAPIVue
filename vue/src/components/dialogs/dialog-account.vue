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
              :rules="nameRules"
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
              :rules="nameRules"
              label="Confirm Password"
              required
          ></v-text-field>
        </v-col>
        <p class="text text-h6 red--text" v-if="err!==null">{{err}}</p>
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
    nameRules: [
      v => !!v || 'Name is required',
      v => v.length <= 10 || 'Name must be less than 10 characters',
    ],
    emailRules: [
      v => !!v || 'E-mail is required',
      v => /.+@.+/.test(v) || 'E-mail must be valid',
    ],
  }),
  methods: {
    LogIn() {
      this.$Auth.LogIn();
      // this.user.LogIn()
      //     .then(response => {
      //       console.log(response);
      //     })
      //     .catch((err) =>{
      //       console.log(err.response.data);
      //     });
    },
    SignUp() {
      this.user.SingUp()
          .then(() => {
            router.push({name: "login"});
          })
          .catch((err) =>{
            this.err=err.response.data;
          });
    }
  }

}
</script>