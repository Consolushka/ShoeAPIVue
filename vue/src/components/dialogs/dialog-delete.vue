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
            @click="dialog = false">
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
            @click="dialog = false"
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
        <alert v-if="snackBar" :snackbar="snackBar" :status="responseFine" @close="snackBar=false"></alert>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import {Brand, Shoe, FilteredShoe} from "../../utils/classes";
import {eventBus} from "../../main";
import axios from "axios";
import {utils} from "../../utils/utils";
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
      this.model.DELETE();
    },
    SendRefresh() {
      eventBus.$emit(`refresh${this.model.ModelName}s`);
    },
    DELETEBrand() {
      axios.delete(utils.API.BRANDS + this.model.Id)
          .then((response) => {
            if (response.status === 204) {
              this.responseFine = true;
              this.snackBar = true;
              setTimeout(this.SendRefresh, 2000);
              console.log(this.responseFine);
            } else {
              this.responseFine = false;
            }
          });
    },
    DELETEShoe() {
      axios.delete(utils.API.SHOES + this.model.Id)
          .then((response) => {
            if (response.status === 204) {
              this.responseFine = true;
              this.snackBar = true;
              setTimeout(this.SendRefresh, 2000);
              console.log(this.responseFine);
            } else {
              this.responseFine = false;
            }
          });
    }
  },
  created() {
    if (this.model instanceof Brand) {
      this.model.ModelName = "Brand";
      this.model.DELETE = this.DELETEBrand;
    } else {
      if (this.model instanceof Shoe || this.model instanceof FilteredShoe) {
        this.model.ModelName = "Shoe";
        this.model.DELETE = this.DELETEShoe;
      }
    }
  }
}
</script>