<template>
  <v-dialog
      v-model="dialog"
      width="500"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn
          color="primary"
          outlined
          plain
          v-bind="attrs"
          v-on="on"
          class="mb-10"
      >
        Create New {{ models.ModelName }}
      </v-btn>
    </template>

    <v-card>
      <v-card-title>
        <span class="text-h5">{{ models.ModelName }} Creation</span>

        <v-spacer></v-spacer>

        <v-btn
            color="black"
            icon
            @click="dialog = false">
          <v-icon>mdi-close</v-icon>
        </v-btn>
      </v-card-title>

      <brand-form v-if="model.ModelName === `Brand`" :brand="model"></brand-form>
      <shoe-form v-if="model.ModelName === 'Shoe'" :shoe="model" :brands="brands"></shoe-form>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
            color="blue darken-1"
            text
            @click="dialog = false">
          Close
        </v-btn>
        <v-btn
            color="blue darken-1"
            text
            @click="Create">
          Create
        </v-btn>
        <alert v-if="snackBar" :snackbar="snackBar" :status="responseFine" @close="snackBar=false"></alert>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import {Brand, Shoe} from "../../utils/classes";
import Alert from "../alert";
import {eventBus} from "../../main";
import BrandForm from "./brand-form";
import ShoeForm from "./shoe-form";
import axios from "axios";
import {utils} from "../../utils/utils";

export default {
  components: {ShoeForm, BrandForm, Alert},
  props: {
    model: Object,
    brands: {
      required: false,
      type: Array
    }
  },
  data(){
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
  methods:{
    Create(){
      console.log(this.model);
      this.model.POST();
    },
    POSTBrand(){
      axios.post(utils.API.BRANDS, {Name: this.model.Name})
          .then((response) => {
            console.log(response.status);
            if (response.status>200 && response.status<300) {
              eventBus.$emit('refresh');
              this.responseFine = true;
              this.snackBar = true;
            } else {
              this.responseFine = false;
            }
          });
    },
    POSTShoe(){
      axios.post(utils.API.SHOES, this.model.ToModel())
          .then((response) => {
            if (response.status === 201) {
              eventBus.$emit('refresh');
              this.responseFine = true;
              this.snackBar = true;
            } else {
              this.responseFine = false;
            }
          });
    }
  },
  created() {
    if (this.model instanceof Brand){
      this.model.ModelName = "Brand";
      this.model.POST = this.POSTBrand;
    }
    else{
      if (this.model instanceof Shoe){
        this.model.ModelName = "Shoe";
        this.model.POST = this.POSTShoe;
      }
    }
  }
}
</script>