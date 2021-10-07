<template>
  <tr>
    <td>
      {{ brand.Id }}
    </td>
    <td> {{ brand.Name }}</td>
    <td>
      <dialog-info :model="brand"></dialog-info>

      <dialog-update :model="brand"></dialog-update>

      <dialog-delete :brand="brand"></dialog-delete>
    </td>
  </tr>
</template>

<script>
import {Brand} from "../utils/classes.js";
import DialogInfo from "./dialogs/dialog-info";
import DialogUpdate from "./dialogs/dialog-update";
import DialogDelete from "./dialogs/dialog-delete";

export default {
  name: "brand",
  components: {DialogDelete, DialogUpdate, DialogInfo},
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
