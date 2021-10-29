<template>
  <section>

    <v-container style="width: 60%">
      <h1 class="text-center text-h3">This is Brand Page</h1>
      <h2 class="text-center text-h4">Here you can change brands</h2>
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
        <brand v-for="brand in $store.getters.BRANDS" :key="brand.Id" :brand="brand"></brand>
        </tbody>
      </template>
    </v-simple-table>
      <dialog-create @refresh="Refresh" :model="selectedBrand" v-if="this.$store.getters.IS_ADMIN"></dialog-create>
    </v-container>

  </section>
</template>

<script>
import {Brand} from "../utils/classes.js";
import brand from '../components/Brand.vue'
import {eventBus} from "../main";
import DialogCreate from "../components/dialogs/dialog-create";

export default {
  name: "brand-page",
  data() {
    return {
      brands: [],
      selectedBrand: new Brand()
    }
  },
  components:{
    DialogCreate,
    brand
  },
  methods: {
    Refresh() {
      this.$store.dispatch('UPDATE_BRANDS');
    //   axios.get(utils.API.BRANDS+"GetAll",{
    //     onDownloadProgress: (progressEvent) => {
    //       const totalLength = progressEvent.lengthComputable ? progressEvent.total : progressEvent.target.getResponseHeader('content-length') || progressEvent.target.getResponseHeader('x-decompressed-content-length');
    //       // console.log("onUploadProgress", totalLength);
    //       if (totalLength !== null) {
    //         // console.log(Math.round( (progressEvent.loaded * 100) / totalLength ));
    //       }
    //     }})
    //     .then((response) => {
    //       this.brands = [];
    //       // console.log("100");
    //       response.data.forEach((brand)=>{
    //         this.brands.push(new Brand(brand));
    //       });
    //     });
    }
  },
  mounted() {
    this.Refresh();
  },
  created() {
    eventBus.$on('refresh', ()=>{
      this.Refresh();
    })
    eventBus.$on('refreshBrands', ()=>{
      this.Refresh();
    })
  }
}
</script>
