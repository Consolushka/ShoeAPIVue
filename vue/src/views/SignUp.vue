<template>
  <v-container style="width: 60%">
    <h1 class="text text-h3 text-center">Sign Up</h1>
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
                v-model="user.UserName"
                label="UserName"
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
          >
            <v-text-field
                v-model="confPass"
                type="password"
                label="Confirm Password"
                required
            ></v-text-field>
          </v-col>
          <p class="text text-h6 red--text" v-if="err!==null">{{ err }}</p>
        </v-row>
        <div></div>
        <v-btn @click="SignUp">Sign Up</v-btn>
      </v-container>
    </v-form>
  </v-container>
</template>
<script>
import {User} from "../utils/classes";
import {eventBus} from "../main";
import router from "../router";

export default {
  data: () => ({
    valid: false,
    err: null,
    user: new User(),
    confPass: "",
    emailRules: [
      v => !!v || 'E-mail is required',
      v => /.+@.+/.test(v) || 'E-mail must be valid',
    ],
  }),
  methods:{
    SignUp() {
      if(this.user.Password !== this.confPass){
        this.err = "Your passwords are not common";
        return;
      }
      this.user.Address = "";
      this.user.Register()
          .then((res) => {
            if(res.status >=200 && res.status<=300){
              eventBus.$emit('showNotification', {responseFine: "Fine", snackBar: true, text: "Signed up successfully"})
              router.push({name: 'login'});
            }
            else{
              eventBus.$emit('showNotification', {responseFine: "Error", snackBar: true, text: res.data})
            }
          });
    }
  }
}
</script>