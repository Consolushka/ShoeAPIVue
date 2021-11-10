<template>
  <v-container style="width: 60%" v-if="$store.getters.ISAUTH">
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
                disabled
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
        <v-btn @click="Update">Update</v-btn>
        <v-btn color="red" style="margin: auto 0" @click="Logout">
          Log Out
        </v-btn>
      </v-container>
    </v-form>
  </v-container>
</template>
<script>
import {eventBus} from "../main";
import router from "../router";
import {User} from "../utils/classes";

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
    Update() {
      if(this.user.Password !== this.confPass){
        this.err = "Your passwords are not common";
        return;
      }
      this.user.Update()
          .then((res) => {
            if(res.status >=200 && res.status<=300){
              eventBus.$emit('showNotification', {responseFine: "Fine", snackBar: true, text: "Confirm changes with our Email"})
            }
            else{
              eventBus.$emit('showNotification', {responseFine: "Error", snackBar: true, text: res.data})
            }
          });
    },
    Logout(){
      this.$store.dispatch('LOGOUT');
      router.push({name: 'brand'});
    }
  },
  mounted(){
    this.user = this.$store.getters.USER;
  }
}
</script>