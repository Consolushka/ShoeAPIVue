<template>
  <section class="d-flex justify-center flex-column">
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

    <div class="modal fade" id="UpdateBrandModal" tabindex="-1" role="dialog" aria-labelledby="UpdateBrandModalLabel"
         aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="UpdateBrandModalLabel">Update Brand</h5>
            <button type="button" class="btn btn-close" data-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="input-group input-group-md mb-3">
              <div class="input-group-prepend">
                <span class="input-group-text" id="ChangeBrandName">Name</span>
              </div>
              <input type="text" v-model="selectedBrand.Name" class="form-control js-cn" aria-label="Small"
                     aria-describedby="ChangeBrandName" :placeholder=selectedBrand.Name>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary" v-on:click="UpdateBrand">Save changes</button>
          </div>
        </div>
      </div>
    </div>

    <div class="modal fade" id="DeleteBrandModal" tabindex="-1" role="dialog" aria-labelledby="DeleteBrandModalLabel"
         aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="DeleteBrandModalLabel">Delete Brand</h5>
            <button type="button" class="btn btn-close" data-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary" v-on:click="DeleteBrand">Delete Brand</button>
          </div>
        </div>
      </div>
    </div>

    <div class="modal fade" id="CreateBrandModal" tabindex="-1" role="dialog" aria-labelledby="CreateNewModalLabel"
         aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="CreateNewModalLabel">Create New Brand</h5>
            <button type="button" class="btn btn-close" data-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="input-group input-group-md mb-3">
              <div class="input-group input-group-md mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text" id="NewBrandName">Name</span>
                </div>
                <input type="text" v-model="selectedBrand.Name" class="form-control js-cn" aria-label="Small"
                       aria-describedby="NewBrandName">
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
              <button type="button" class="btn btn-primary" v-on:click="CreateBrand">Create new</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import {utils} from "../utils/utils.js"
import {Brand} from "../utils/classes.js";
import axios from 'axios'
import brand from '../components/brand.vue'

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
    UpdateBrand() {
      axios.put(utils.API.BRANDS + this.selectedBrand.Id, this.selectedBrand).then((response) => {
        if (response.status === 204) {
          this.refreshBrands();
        }
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
      axios.post(utils.API.BRANDS, {
        Name: this.selectedBrand.Name
      })
        .then((response) => {
          if (response.status === 201) {
            this.refreshBrands();
            utils.CloseModal("Create", "Brand");
          }
          else{
            console.log(response);
          }
        });
    },
    SwitchSelectedBrand(brand){
      this.selectedBrand = new Brand(brand);
    }
  },
  mounted() {
    this.refreshBrands();
  }
}
</script>
