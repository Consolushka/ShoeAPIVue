<template>
  <v-card
      :loading="loading"
      class=""
      v-if="shoe.matched"
  >
    <template slot="progress">
      <v-progress-linear
          color="deep-purple"
          height="10"
          indeterminate
      ></v-progress-linear>
    </template>

    <v-img
        cover
        width="100%"
        height="350px"
        :src="`${photoUrl}${shoe.PhotoFileName}`"
        :alt="shoe.Name"
    ></v-img>

    <v-card-title>{{shoe.Name}}</v-card-title>

    <v-card-text>

      <div class="my-4 text-subtitle-1">
        {{shoe.Brand.Name}}
      </div>

      <div>{{shoe.CreationTime}}</div>
    </v-card-text>

    <v-divider class="mx-4"></v-divider>

    <v-card-actions>

      <dialog-info :model="shoe" :brands="brands"></dialog-info>

      <dialog-update :model="shoe" :brands="brands"></dialog-update>

      <dialog-delete :model="shoe" :brands="brands"></dialog-delete>

    </v-card-actions>
  </v-card>
</template>

<script>
import {utils} from "../utils/utils.js"
import {Shoe} from "../utils/classes.js";
import DialogInfo from "./dialogs/dialog-info";
import DialogUpdate from "./dialogs/dialog-update";
import DialogDelete from "./dialogs/dialog-delete"

export default {
  name: "Shoe",
  components: {DialogUpdate, DialogInfo, DialogDelete},
  props: {
    shoe: Shoe,
    brands: Array
  },
  data(){
    return{
      photoUrl: utils.PHOTO_URL,
      loading: false,
      selection: 1,
    }
  },
  methods:{
    SwitchSelectedShoe(shoe) {
      this.$emit("switchShoe", shoe);
    }
  }

}
</script>
