<template>
  <v-dialog
      v-model="dialog"
      width="600"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn
          color="green"
          icon
          plain
          tile
          v-bind="attrs"
          v-on="on"
      >
        <v-icon>mdi-information</v-icon>
      </v-btn>
    </template>

    <v-card>
      <v-card-title>
        <span class="text-h5">More information of {{ model.ModelName }}</span>

        <v-spacer></v-spacer>

        <v-btn
            color="black"
            icon
            @click="dialog = false"
        >
          <v-icon>mdi-close</v-icon>
        </v-btn>
      </v-card-title>

      <v-card-text>
        <brand-form v-if="model.ModelName === `Brand`" :brand="model" :disabled="true"></brand-form>
        <shoe-form v-if="model.ModelName === 'Shoe'" :shoe="model" :brands="brands" :disabled="true"></shoe-form>
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script>
import {Brand, Shoe, FilteredShoe} from "../../utils/classes";
import ShoeForm from "./shoe-form";
import BrandForm from "./brand-form";


export default {
  name: "dialog-info",
  components: {ShoeForm, BrandForm},
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
    }
  },
  created() {
    if (this.model instanceof Brand){
      this.model.ModelName = "Brand";
    }
    else{
      if (this.model instanceof Shoe || this.model instanceof FilteredShoe){
        this.model.ModelName = "Shoe";
      }
    }
  }
}
</script>