<template>
  <v-dialog
      v-model="dialog"
      width="600"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn
          color="red"
          icon
          plain
          tile
          v-bind="attrs"
          v-on="on"
      >
        <v-icon>mdi-delete</v-icon>
      </v-btn>
    </template>

    <v-card>
      <v-card-title>
        <span class="text-h5">Delete {{ model.ModelName }}</span>

        <v-spacer></v-spacer>

        <v-btn
            color="black"
            icon
            @click="Close">
          <v-icon>mdi-close</v-icon>
        </v-btn>
      </v-card-title>

      <v-card-text>
        <brand-form v-if="model.ModelName === `Brand`" :brand="model" :disabled="true"></brand-form>
        <shoe-form v-if="model.ModelName === 'Shoe'" :shoe="model" :brands="brands" :disabled="true"></shoe-form>
      </v-card-text>


      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
            color="blue darken-1"
            text
            @click="Close"
        >
          Close
        </v-btn>
        <v-btn
            color="blue darken-1"
            text
            @click="Delete"
        >
          Delete
        </v-btn>
        <alert v-if="snackBar" :snackbar="snackBar" :status="responseFine" @close="Close" @force="ForcedDeletion"></alert>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import {eventBus} from "../../main";
import Alert from "../alert";
import BrandForm from "./brand-form";
import ShoeForm from "./shoe-form"

export default {
  name: "dialog-update",
  components: {ShoeForm, BrandForm, Alert},
  props: {
    model: Object,
    brands: {
      required: false,
      type: Array
    }
  },
  data() {
    return {
      dialog: false,
      responseFine: true,
      snackBar: false,
      models: {
        Name: "",
        POST: null
      }
    }
  },
  methods: {
    Delete() {
      this.model.DELETE(this.$store.getters.CONFIG_HEADER).then(status => {
        this.responseFine = status;
        this.snackBar = true;
        setTimeout(this.SendRefresh, 2000);
        console.log(this.responseFine);
      })
          .catch((err) => {
            console.log(err.response.status);
            this.snackBar = true;
            this.responseFine = err.response.status;
          })
    },
    SendRefresh() {
      eventBus.$emit(`refresh${this.model.ModelName}s`);
    },
    ForcedDeletion(){
      this.snackBar = false;
      this.model.DELETEForce().then(status => {
        this.responseFine = status;
        this.snackBar = true;
        setTimeout(this.SendRefresh, 2000);
        console.log(this.responseFine);
      })
          .catch((err) => {
            console.log(err.response.status);
            this.snackBar = true;
            this.responseFine = err.response.status;
          })
    },
    Close(){
      this.dialog = false;
      this.snackBar = false;
    }
  }
}
</script>