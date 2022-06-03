<template>
  <tr>
    <td>
      {{ type.Id }}
    </td>
    <td> {{ type.Name }}</td>
    <td>
      <dialog-info :model="type"></dialog-info>

      <dialog-update :model="CopyBrand()" v-if="this.$store.getters.IS_ADMIN"></dialog-update>

      <dialog-delete :model="type" v-if="this.$store.getters.IS_ADMIN"></dialog-delete>
    </td>
  </tr>
</template>

<script>
import {Type} from "../utils/classes.js";
import DialogInfo from "./dialogs/dialog-info";
import DialogUpdate from "./dialogs/dialog-update";
import DialogDelete from "./dialogs/dialog-delete";

export default {
  name: "type",
  components: {DialogDelete, DialogUpdate, DialogInfo},
  props: {
    type: Type
  },
  data() {
    return {
      dialog: false,
      selectedBrand: this.CopyBrand()
    }
  },
  methods:{
    CopyBrand(){
      return new Type(this.type.ToModel());
    }
  },
  created() {
    this.$on('rollback',(type)=>{
      return new Type(type.ToModel());
    })
  }
}
</script>