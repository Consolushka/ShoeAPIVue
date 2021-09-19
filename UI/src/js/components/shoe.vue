<template>
  <section>
    <h1>This is Shoes Page</h1>
    <h2>Here you can change shoes</h2>
    <button type="button" class="btn btn-primary mb-3" data-toggle="modal" data-target="#CreateShoeModal">
      Create new Shoe
    </button>
    <section class="cards-collection">
      <article v-for="shoe in shoes" class="card">
        <img height="350" :src="`${photoUrl}${shoe.PhotoFileName}`" :alt="shoe.Name" class="card-image">
        <h4 class="card-name">{{ shoe.Name }}</h4>
        <h3 class="card-brand">{{ shoe.Brand.Name }}</h3>
        <ul class="card-options d-flex justify-content-center">
          <li class="option">
            <button class="btn btn-light" v-on:click="SwitchSelectedShoe(shoe)" data-toggle="modal"
                    data-target="#InfoShoeModal">
              <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                   class="bi bi-info-square" viewBox="0 0 16 16">
                <path
                  d="M14 1a1 1 0 0 1 1 1v12a1 1 0 0 1-1 1H2a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1h12zM2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2z"/>
                <path
                  d="m8.93 6.588-2.29.287-.082.38.45.083c.294.07.352.176.288.469l-.738 3.468c-.194.897.105 1.319.808 1.319.545 0 1.178-.252 1.465-.598l.088-.416c-.2.176-.492.246-.686.246-.275 0-.375-.193-.304-.533L8.93 6.588zM9 4.5a1 1 0 1 1-2 0 1 1 0 0 1 2 0z"/>
              </svg>
            </button>
          </li>
          <li class="option">
            <button type="button" class="btn btn-light mr-1"
                    v-on:click="SwitchSelectedShoe(shoe)" data-toggle="modal"
                    data-target="#UpdateBrandModal">
              <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                   class="bi bi-pencil-square" viewBox="0 0 16 16">
                <path
                  d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                <path fill-rule="evenodd"
                      d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
              </svg>
            </button>
          </li>
          <li class="option">
            <button type="button" class="btn btn-light" v-on:click="SwitchSelectedShoe(shoe)" data-toggle="modal"
                    data-target="#DeleteShoeModal">
              <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash"
                   viewBox="0 0 16 16">
                <path
                  d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"/>
                <path fill-rule="evenodd"
                      d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"/>
              </svg>
            </button>
          </li>
        </ul>
      </article>
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
                       aria-describedby="UpdateShoeName" >
              </div>

              <select class="form-select form-select-sm mb-3" aria-label=".form-select-sm example" v-model="selectedShoe.Brand.Id">
                <option value="null">Open this select menu</option>
                <option v-for="brand in brands" :value="brand.Id">{{brand.Name}}</option>
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
                       aria-describedby="DeleteShoeCreation" :placeholder=ConvertDate(selectedShoe.CreationTime) disabled>
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

                <select class="form-select form-select-sm mb-3" aria-label=".form-select-sm example" v-model="selectedShoe.Brand.Id">
                  <option value="null" selected>Open this select menu</option>
                  <option v-for="brand in brands" :value="brand.Id">{{brand.Name}}</option>
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
                       aria-describedby="InfoShoeCreation" :placeholder=ConvertDate(selectedShoe.CreationTime) disabled>
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
  object-fit: cover;
}

.card-options{
  list-style: none;
  padding-left: 0;
}

.option{
  margin-right: 15px;
}
</style>

<script>
import {utils} from "../utils.js"
import {Shoe} from "../classes.js";

export default {
  name: "shoe",
  data() {
    return {
      shoes: [],
      selectedShoe: new Shoe(),
      photoUrl: utils.PHOTO_URL,
      brands: []
    }
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
          this.shoes = [];
          // console.log("100");
          response.data.forEach((shoe) => {
            this.shoes.push(new Shoe(shoe));
          });
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
    ConvertDate(date) {
      function pad(s) {
        return (s < 10) ? '0' + s : s;
      }

      let d = new Date(date)
      return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('.')
    },
    SwitchSelectedShoe(shoe) {
      this.selectedShoe = new Shoe(shoe);
    },
    ImageUpload(e){
      let form = new FormData();
      form.append("file", e.target.files[0]);
      axios.post(`${utils.API.SHOES}SaveFile`, form)
        .then((response)=>{
          this.selectedShoe.PhotoFileName = response.data;
        });
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
