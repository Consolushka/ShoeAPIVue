<template>
  <section>
    <v-container style="width: 60%">
      <h1 class="text-center text-h3">This is Types Page</h1>
      <h2 class="text-center text-h4">Here you can change types</h2>
      <v-simple-table class="mb-10">
        <template v-slot:default>
          <thead>
          <tr>
            <th class="text-left">
              Id
            </th>
            <th class="text-left">
              Name
            </th>
            <th class="text-left">
              Options
            </th>
          </tr>
          </thead>
          <tbody>
          <type v-for="type in $store.getters.TYPES" :key="type.Id" :type="type"></type>
          </tbody>
        </template>
      </v-simple-table>
      <dialog-create @refresh="Refresh" :model="selectedBrand" v-if="this.$store.getters.IS_ADMIN"></dialog-create>
    </v-container>

  </section>
</template>

<script>
import {Type} from "../utils/classes.js";
import type from '../components/type.vue'
import {eventBus} from "../main";
import DialogCreate from "../components/dialogs/dialog-create";

export default {
  name: "brand-page",
  data() {
    return {
      selectedBrand: new Type()
    }
  },
  components:{
    DialogCreate,
    type
  },
  methods: {
    Refresh() {
      this.$store.dispatch('UPDATE_TYPES');
    }
  },
  created() {
    console.log(this.$store.getters.IS_ADMIN);
    eventBus.$on('refresh', ()=>{
      this.Refresh();
    })
    eventBus.$on('refreshBrands', ()=>{
      this.Refresh();
    })
  }
}
</script>
