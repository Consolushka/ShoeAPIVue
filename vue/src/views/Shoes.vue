<template>
  <section>
    <h1>This is Shoes Page</h1>
    <h2>Here you can change shoes</h2>
    <div class="accordion" id="accordionExample">
      <div class="card w-100">
        <div class="card-header" id="headingOne">
          <h2 class="mb-0">
            <button class="btn btn-link btn-block text-left" type="button" data-toggle="collapse" data-target="#Filters"
                    aria-expanded="false" aria-controls="collapseOne">
              Filters
            </button>
          </h2>
        </div>

        <div id="Filters" class="collapse hide" aria-labelledby="headingOne" data-parent="#accordionExample">
          <div class="card-body">
            <div class="form-group d-flex">
              <select class="filter__select filter__param" v-model="filterParam.BrandId">
                <option selected :value="null">Choose brand you need</option>
                <option v-for="brand in brands" :key="brand.Id" :value="brand.Id">{{brand.Name}}</option>
              </select>
              <div class="input-group mb-3 filter__param">
                <div class="input-group-prepend">
                  <span class="input-group-text" id="FilterName">Name</span>
                </div>
                <input v-model="filterParam.Name" type="text" class="form-control" placeholder="Name" aria-label="Brand"
                       aria-describedby="FilterName">
              </div>
              <div class="input-group mb-3 filter__param">
                <div class="input-group-prepend">
                  <span class="input-group-text" id="FilterCreationDate">CreationDate</span>
                </div>
                <input @change="SetFilterDate" type="date" class="form-control" placeholder="CreationDate" aria-label="CreationDate"
                       aria-describedby="CreationDate">
              </div>
            </div>
            <button class="btn btn-primary" @click="RenderFilters">Filter</button>
            <button class="btn btn-secondary" type="button" @click="ClearFilters">Clear</button>
          </div>
        </div>
      </div>
    </div>
    <button type="button" class="btn btn-primary mb-3" @click="SwitchSelectedShoe()" data-toggle="modal"
            data-target="#CreateShoeModal">
      Create new Shoe
    </button>
    <section class="cards-collection">
      <shoe v-for="shoe in RenderedShoes" :key="shoe.Id" v-bind:shoe="shoe" @switchShoe="SwitchSelectedShoe"></shoe>
    </section>

    <div class="modal fade" id="UpdateBrandModal" tabindex="-1" role="dialog" aria-labelledby="UpdateBrandModalLabel"
         aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="UpdateBrandModalLabel">Update Shoe</h5>
            <button type="button" class="btn btn-close" data-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body row">
            <div class="col-6">
              <div class="input-group input-group-sm mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text" id="UpdateShoeName">Name</span>
                </div>
                <input type="text" v-model="selectedShoe.Name" class="form-control js-cn" aria-label="Small"
                       aria-describedby="UpdateShoeName">
              </div>

              <select class="form-select form-select-sm mb-3" aria-label=".form-select-sm example"
                      v-model="selectedShoe.Brand.Id">
                <option value="null">Open this select menu</option>
                <option v-for="brand in brands" :key="brand.Id" :value="brand.Id">{{ brand.Name }}</option>
              </select>

              <div class="input-group input-group-sm mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text" id="UpdateShoeCreation">Creation Time</span>
                </div>
                <input type="date" v-model="selectedShoe.CreationTime" class="form-control js-cn" aria-label="Small"
                       aria-describedby="UpdateShoeCreation">
              </div>
            </div>
            <div class="col-6 d-flex justify-content-center flex-column">
              <img :src="`${photoUrl}${selectedShoe.PhotoFileName}`" height="200px"
                   style="object-fit: contain" alt="">
              <input type="file" v-on:change="ImageUpload">
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary" v-on:click="UpdateShoe">Save changes</button>
          </div>
        </div>
      </div>
    </div>

    <div class="modal fade" id="DeleteShoeModal" tabindex="-1" role="dialog" aria-labelledby="DeleteShoeModalLabel"
         aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="DeleteShoeModalLabel">Delete Shoe</h5>
            <button type="button" class="btn btn-close" data-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body row">
            <div class="col-6">
              <div class="input-group input-group-sm mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text" id="DeleteShoeId">Id</span>
                </div>
                <input type="text" class="form-control js-cn" aria-label="Small"
                       aria-describedby="DeleteShoeId" :placeholder=selectedShoe.Id disabled>
              </div>
              <div class="input-group input-group-sm mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text" id="DeleteShoeName">Name</span>
                </div>
                <input type="text" class="form-control js-cn" aria-label="Small"
                       aria-describedby="DeleteShoeName" :placeholder=selectedShoe.Name disabled>
              </div>

              <div class="input-group input-group-sm mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text" id="DeleteShoeBrand">Brand</span>
                </div>
                <input type="text" class="form-control js-cn" aria-label="Small"
                       aria-describedby="DeleteShoeBrand" :placeholder=selectedShoe.Brand.Name disabled>
              </div>

              <div class="input-group input-group-sm mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text" id="DeleteShoeCreation">Creation Time</span>
                </div>
                <input type="text" class="form-control js-cn" aria-label="Small"
                       aria-describedby="DeleteShoeCreation" :placeholder=selectedShoe.CreationTime
                       disabled>
              </div>
            </div>
            <div class="col-6 d-flex justify-content-center">
              <img :src="`${photoUrl}${selectedShoe.PhotoFileName}`" height="200px"
                   style="object-fit: cover; max-width: 100%;" alt="">
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary" v-on:click="DeleteShoe">Delete Shoe</button>
          </div>
        </div>
      </div>
    </div>

    <div class="modal fade" id="CreateShoeModal" tabindex="-1" role="dialog" aria-labelledby="CreateShoeModalLabel"
         aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="CreateShoeModalLabel">Create New Shoe</h5>
            <button type="button" class="btn btn-close" data-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="modal-body row">
              <div class="col-6">
                <div class="input-group input-group-sm mb-3">
                  <div class="input-group-prepend">
                    <span class="input-group-text" id="CreateShoeName">Name</span>
                  </div>
                  <input type="text" v-model="selectedShoe.Name" class="form-control js-cn" aria-label="Small"
                         aria-describedby="CreateShoeName">
                </div>

                <select class="form-select form-select-sm mb-3" aria-label=".form-select-sm example"
                        v-model="selectedShoe.Brand.Id">
                  <option value="null" selected>Open this select menu</option>
                  <option v-for="brand in brands" :key="brand.Id" :value="brand.Id">{{ brand.Name }}</option>
                </select>

                <div class="input-group input-group-sm mb-3">
                  <div class="input-group-prepend">
                    <span class="input-group-text" id="CreateShoeCreation">Creation Time</span>
                  </div>
                  <input type="date" v-model="selectedShoe.CreationTime" class="form-control js-cn" aria-label="Small"
                         aria-describedby="CreateShoeCreation">
                </div>
              </div>
              <div class="col-6 d-flex justify-content-center flex-column">
                <img :src="`${photoUrl}${selectedShoe.PhotoFileName}`" height="200px"
                     style="object-fit: contain; text-align: center" alt="">
                <input type="file" v-on:change="ImageUpload">
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-primary" v-on:click="CreateShoe">Create new</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="modal fade" id="InfoShoeModal" tabindex="-1" role="dialog" aria-labelledby="InfoShoeModalLabel"
         aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="InfoShoeModalLabel">More Information Of Shoe</h5>
            <button type="button" class="btn btn-close" data-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body row">
            <div class="col-6">
              <div class="input-group input-group-sm mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text" id="InfoShoeId">Id</span>
                </div>
                <input type="text" class="form-control js-cn" aria-label="Small"
                       aria-describedby="InfoShoeId" :placeholder=selectedShoe.Id disabled>
              </div>
              <div class="input-group input-group-sm mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text" id="InfoShoeName">Name</span>
                </div>
                <input type="text" class="form-control js-cn" aria-label="Small"
                       aria-describedby="InfoShoeName" :placeholder=selectedShoe.Name disabled>
              </div>

              <div class="input-group input-group-sm mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text" id="InfoShoeBrand">Brand</span>
                </div>
                <input type="text" class="form-control js-cn" aria-label="Small"
                       aria-describedby="InfoShoeBrand" :placeholder=selectedShoe.Brand.Name disabled>
              </div>

              <div class="input-group input-group-sm mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text" id="InfoShoeCreation">Creation Time</span>
                </div>
                <input type="text" class="form-control js-cn" aria-label="Small"
                       aria-describedby="InfoShoeCreation" :placeholder=selectedShoe.CreationTime disabled>
              </div>
            </div>
            <div class="col-6 d-flex justify-content-center">
              <img :src="`${photoUrl}${selectedShoe.PhotoFileName}`" height="200px"
                   style="object-fit: cover;  max-width: 100%;" alt="">
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style>
.card {
  width: 23.5%;
  padding: 5px;
  border: 1px solid black;
  margin-right: 15px;
  margin-bottom: 30px;
}

.cards-collection {
  display: flex;
  justify-content: flex-start;
  flex-wrap: wrap;
}

.card-image {
  object-fit: contain;
}

.card-options {
  list-style: none;
  padding-left: 0;
}

.option {
  margin-right: 15px;
}

.filter__param{
  margin-right: 15px;
  width: 30%;
}

.filter__select{
  max-height: 36px;
  border: 1px solid #ced4da;
  border-radius: 4px;
  padding: 0 12px;
}
</style>

<script>
import {utils} from "../utils/utils.js"
import {Shoe, FilteredShoe} from "../utils/classes.js";
import axios from 'axios'
import shoe from "../components/shoe.vue"

export default {
  name: "shoe-page",
  data() {
    return {
      starterShoes: [],
      RenderedShoes: [],
      selectedShoe: new Shoe(),
      photoUrl: utils.PHOTO_URL,
      brands: [],
      filterParam: {
        BrandId: null,
        Name: null,
        CreationDate: null
      }
    }
  },
  components:{
    shoe
  },
  methods: {
    RefreshShoes() {
      axios.get(utils.API.SHOES, {
        onDownloadProgress: (progressEvent) => {
          const totalLength = progressEvent.lengthComputable ? progressEvent.total : progressEvent.target.getResponseHeader('content-length') || progressEvent.target.getResponseHeader('x-decompressed-content-length');
          // console.log("onUploadProgress", totalLength);
          if (totalLength !== null) {
            // console.log(Math.round( (progressEvent.loaded * 100) / totalLength ));
          }
        }
      })
        .then((response) => {
          this.starterShoes = [];
          // console.log("100");
          response.data.forEach((shoe) => {
            let curr = new Shoe(shoe);
            curr.CreationTime = utils.ConvertDate(curr.CreationTime);
            this.starterShoes.push(curr);
            this.RenderedShoes.push(new FilteredShoe(curr));
          });
          console.log(this.RenderedShoes);
        });
    },
    UpdateShoe() {
      axios.put(utils.API.SHOES + this.selectedShoe.Id, this.selectedShoe.ToModel()).then((response) => {
        if (response.status === 204) {
          this.RefreshShoes();
        }
      });
    },
    DeleteShoe() {
      axios.delete(utils.API.SHOES + this.selectedShoe.Id)
        .then((response) => {
          if (response.status === 204) {
            this.RefreshShoes();
            utils.CloseModal("Delete", "Shoe");
          }
        });
    },
    CreateShoe() {
      axios.post(utils.API.SHOES, this.selectedShoe.ToModel())
        .then((response) => {
          if (response.status === 201) {
            this.RefreshShoes();
            utils.CloseModal("Create", "Shoe");
          } else {
            console.log(response);
          }
        });
    },
    SwitchSelectedShoe(shoe) {
      this.selectedShoe = new Shoe(shoe);
    },
    ImageUpload(e) {
      let form = new FormData();
      form.append("file", e.target.files[0]);
      axios.post(`${utils.API.SHOES}SaveFile`, form)
        .then((response) => {
          this.selectedShoe.PhotoFileName = response.data;
        });
    },
    RenderFilters(){
      console.log(this.filterParam);
      this.RenderedShoes.forEach((shoe)=>{
        shoe.MatchFilter(this.filterParam);
      });
      console.log(this.RenderedShoes);
    },
    SetFilterDate(e){
      let date = e.target.value.toString();
      let splited = date.split('-');
      let res = "";
      for(let i=splited.length-1;i>=0;i--){
        res+=splited[i]+"-";
      }
      res = res.slice(0,res.length-1);
      this.filterParam.CreationDate = res;
    },
    ClearFilters(){
      this.filterParam = {
        BrandId: null,
          Name: null,
          CreationDate: null
      };
      this.RenderFilters();
    }
  },
  mounted() {
    this.RefreshShoes();
    axios.get(utils.API.BRANDS)
      .then((response) => {
        this.brands = [];
        // console.log("100");
        response.data.forEach((brand) => {
          this.brands.push(new Shoe(brand));
        });
      });
  }
}
</script>
