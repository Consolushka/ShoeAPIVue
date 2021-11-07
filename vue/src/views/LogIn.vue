<template>
  <v-container style="width: 60%">
    <h1 class="text text-h3 text-center">Log In</h1>
    <v-form v-model="valid">
      <v-container>
        <v-row>
          <v-col
              cols="12"
          >
            <v-text-field
                v-model="user.Login"
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
          <p class="text text-h6 red--text" v-if="err!==null">{{ err }}</p>
        </v-row>
        <div></div>
        <v-btn @click="LogIn">Log In</v-btn>
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
  }
}
</script>