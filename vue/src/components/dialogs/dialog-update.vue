<template>
  <v-dialog
      v-model="dialog"
      width="600"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn
          color="primary"
          icon
          plain
          tile
          v-bind="attrs"
          v-on="on"
      >
        <v-icon>mdi-pencil-outline</v-icon>
      </v-btn>
    </template>

    <v-card>
      <v-card-title>
        <span class="text-h5">Update {{ model.ModelName }}</span>

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
            @click="Update">
          Update
        </v-btn>
        <alert v-if="snackBar" :snackbar="snackBar" :status="responseFine" @close="snackBar=false"></alert>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
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
    Update(){
      this.model.PUT(this.$store.getters.CONFIG_HEADER).then(status =>{
        if (status < 300 && status>=200) {
          this.responseFine = status;
          this.snackBar = true;
        }
        else{
          this.responseFine = false;
        }
      })
    }
  }
}
</script>