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
            @click="dialog = false;">
          Close
        </v-btn>
        <v-btn
            color="blue darken-1"
            text
            @click="Update">
          Update
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import BrandForm from "./brand-form";
import ShoeForm from "./shoe-form"
import {eventBus} from "../../main";
import {Brand, Shoe} from "../../utils/classes";

export default {
  name: "dialog-update",
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
      startedModel: "",
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
      this.model.PUT(this.$store.getters.CONFIG_HEADER).then(response =>{
        let status = response.status;
        if (status < 300 && status>=200) {
          this.$store.dispatch(`UPDATE_${this.model.ModelName.toUpperCase()}S`);
          eventBus.$emit('showNotification', {responseFine: "Fine", snackBar: true, text: "Fine"});
        }
        else{
          eventBus.$emit('showNotification', {responseFine: "Error", snackBar: false, text: response.data});
        }
      })
    },
    Close(){
      this.$emit('rollback', this.startedModel);
      console.log(this.$store.getters.BRANDS);
    }
  },
  mounted() {
    if(this.model.ModelName === "Shoe"){
      this.startedModel = new Shoe(this.model.ToModel());
    }
    else{
      this.startedModel = new Brand(this.model.ToModel())
    }
    console.log(this.startedModel);
  }
}
</script>