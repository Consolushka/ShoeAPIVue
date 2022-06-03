<template>
  <v-container style="width: 60%" v-if="$store.getters.ISAUTH">
    <h1 class="text text-h3 text-center">Your Account</h1>
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
                v-model="user.Address"
                label="Address"
            ></v-text-field>
          </v-col>
          <v-dialog
              v-model="dialog"
              persistent
              max-width="600px"
          >
            <template v-slot:activator="{ on, attrs }">
              <v-btn
                  color="primary"
                  dark
                  v-bind="attrs"
                  v-on="on"
              >
                Change Password
              </v-btn>
            </template>
            <v-card>
              <v-card-title>
                <span class="text-h5">User Profile</span>
              </v-card-title>
              <v-card-text>
                <v-container>
                  <v-col
                      cols="12">
                    <v-text-field
                        v-model="newPassword"
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
                </v-container>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn
                    color="blue darken-1"
                    text
                    @click="dialog = false"
                >
                  Close
                </v-btn>
                <v-btn
                    color="blue darken-1"
                    text
                    @click="ChangePassword"
                >
                  Save
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-row>
      </v-container>
      <v-btn @click="Update">Update</v-btn>
      <v-btn color="red" style="margin: auto 0" @click="Logout">
        Log Out
      </v-btn>
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
    dialog: false,
    user: new User(),
    newPassword: "",
    confPass: "",
    isChangePass: false,
    emailRules: [
      v => !!v || 'E-mail is required',
      v => /.+@.+/.test(v) || 'E-mail must be valid',
    ],
  }),
  methods: {
    Update() {
      console.log(this.user);
      this.user.Update(this.$store.getters.CONFIG_HEADER)
          .then((res) => {
            console.log(res);
            if (res.status >= 200 && res.status <= 300) {
              eventBus.$emit('showNotification', {
                responseFine: "Fine",
                snackBar: true,
                text: "Confirm changes with our Email"
              })
            } else {
              eventBus.$emit('showNotification', {responseFine: "Error", snackBar: true, text: res.data.message})
            }
          })
          .catch(err => {
            console.log(err);
          });
    },
    Logout() {
      this.$store.dispatch('LOGOUT');
      router.push({name: 'brand'});
    },
    ChangePassword(){
      console.log(this.newPassword);
      console.log(this.confPass);
      console.log(this.user);
      this.user.ChangePassword(this.newPassword, this.$store.getters.CONFIG_HEADER);
    }
  },
  mounted() {
    this.user = this.$store.getters.USER;
    console.log(this.user);
  }
}
</script>