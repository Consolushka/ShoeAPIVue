<template>
  <section>
    <h1>This is Brand Page</h1>
    <h2>Here you can change brands</h2>

    <v-simple-table>
      <template v-slot:default>
        <thead>
        <tr>
          <th class="text-left">
            Id
          </th>
          <th class="text-left">
            Name
          </th>
          <th>
            Options
          </th>
        </tr>
        </thead>
        <tbody>
          <brand v-for="brand in brands" :key="brand.Id" :brand="brand"></brand>
        </tbody>
      </template>
    </v-simple-table>
    <dialog-create @refreshBrands="refreshBrands"></dialog-create>
  </section>
</template>

<script>
import {utils} from "../utils/utils.js"
import {Brand} from "../utils/classes.js";
import axios from 'axios'
import brand from '../components/brand.vue'
import {eventBus} from "../main";
import DialogCreate from "../components/dialogs/dialog-create";

export default {
  name: "brand-page",
  data() {
    return {
      brands: [],
      selectedBrand: new Brand(),
      headers: [
        { text: 'Id', value: 'Id' },
        { text: 'Name', value: 'Name' },
        { text: 'Options', value: 'Options' }
      ]
    }
  },
  components:{
    DialogCreate,
    brand
  },
  methods: {
    refreshBrands() {
      axios.get(utils.API.BRANDS,{
        onDownloadProgress: (progressEvent) => {
          const totalLength = progressEvent.lengthComputable ? progressEvent.total : progressEvent.target.getResponseHeader('content-length') || progressEvent.target.getResponseHeader('x-decompressed-content-length');
          // console.log("onUploadProgress", totalLength);
          if (totalLength !== null) {
            // console.log(Math.round( (progressEvent.loaded * 100) / totalLength ));
          }
        }})
        .then((response) => {
          this.brands = [];
          // console.log("100");
          response.data.forEach((brand)=>{
            this.brands.push(new Brand(brand));
          });
        });
    },
    DeleteBrand() {
      axios.delete(utils.API.BRANDS + this.selectedBrand.Id)
        .then((response) => {
          if (response.status === 204) {
            this.refreshBrands();
            utils.CloseModal("Delete", "Brand");
          }
        });
    },
    CreateBrand(){
    },
    SwitchSelectedBrand(brand){
      this.selectedBrand = new Brand(brand);
    }
  },
  mounted() {
    this.refreshBrands();
  },
  created() {
    eventBus.$on('refreshBrands', ()=>{
      this.refreshBrands();
    })
  }
}
</script>
