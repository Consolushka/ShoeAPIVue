<template>
  <tr>
    <td>
      {{ brand.Id }}
    </td>
    <td> {{ brand.Name }}</td>
    <td>
      <dialog-info :brand="brand"></dialog-info>

      <dialog-update :brand="brand"></dialog-update>

      <v-btn
          icon
          plain
          tile
          color="red"
          v-on:click="$emit('SwitchSelectedBrand',brand)"
          data-toggle="modal"
          data-target="#DeleteBrandModal"
      >
       <v-icon>mdi-delete</v-icon>
      </v-btn>
    </td>
  </tr>
</template>

<script>
import {Brand} from "../utils/classes.js";
import DialogInfo from "./dialog-info";
import DialogUpdate from "./dialog-update";

export default {
  name: "brand",
  components: {DialogUpdate, DialogInfo},
  props: {
    brand: Brand
  },
  data() {
    return {
      dialog: false,
    }
  }
}
</script>
