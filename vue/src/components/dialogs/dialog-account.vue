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
      </v-row>
      <v-btn @click="LogIn" v-if="isLogin">Log In</v-btn>
      <v-btn @click="SignUp" v-else>Sign Up</v-btn>
    </v-container>
  </v-form>
</template>

<script>

import {User} from "../../utils/classes";

export default {
  name: "dialog-account",
  props:{
    isLogin: Boolean
  },
  data: () => ({
    valid: false,
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
  methods:{
    LogIn(){
        this.user.LogIn();
    },
    SignUp(){
      this.user.SingUp();
    }
  }

}
</script>