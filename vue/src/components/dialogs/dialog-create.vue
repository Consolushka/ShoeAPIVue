<template>
  <v-dialog
      v-model="dialog"
      width="600"
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
        Create New {{ model.ModelName }}
      </v-btn>
    </template>

    <v-card>
      <v-card-title>
        <span class="text-h5">{{ model.ModelName }} Creation</span>

        <v-spacer></v-spacer>

        <v-btn
            color="black"
            icon
            @click="dialog = false">
          <v-icon>mdi-close</v-icon>
        </v-btn>
      </v-card-title>

      <v-card-text>
        <brand-form v-if="model.ModelName === `Brand`" :brand="model"></brand-form>
        <shoe-form v-if="model.ModelName === 'Shoe'" :shoe="model" :brands="brands"></shoe-form>
      </v-card-text>

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
import Alert from "../alert";
// import {eventBus} from "../../main";
import BrandForm from "./brand-form";
import ShoeForm from "./shoe-form";

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
      this.model.POST();
      // this.model.POST().then(status=>{
      //   if(status>=200 && status<300){
      //     eventBus.$emit(`refresh${this.model.ModelName}s`);
      //     this.responseFine = status;
      //     this.snackBar = true;
      //   }
      //   else{
      //     this.responseFine = status;
      //   }
      // });

    }
  }
}
</script>