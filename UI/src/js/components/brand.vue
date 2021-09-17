<template>
  <section>
    <h1>This is Brand Page</h1>
    <h2>Here you can change brands</h2>
    <table class="table table-stripped">
      <thead>
      <tr>
        <th>Id</th>
        <th>Name</th>
        <th>Options</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="brand in this.brands">
        <td>
          {{ brand.Id }}
        </td>
        <td> {{ brand.Name }}</td>
        <td>
          <button type="button" class="btn btn-light mr-1"
                  v-on:click="changingBrand.Id = brand.Id; changingBrand.Name = brand.Name" data-toggle="modal"
                  data-target="#UpdateBrandModal">
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                 class="bi bi-pencil-square" viewBox="0 0 16 16">
              <path
                d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
              <path fill-rule="evenodd"
                    d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
            </svg>
          </button>
          <button type="button" class="btn btn-light" v-on:click="changingBrand.Id = brand.Id" data-toggle="modal"
                  data-target="#DeleteBrandModal">
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash"
                 viewBox="0 0 16 16">
              <path
                d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"/>
              <path fill-rule="evenodd"
                    d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"/>
            </svg>
          </button>
        </td>
      </tr>
      <tr>
        <td>
          <button type="button" class="btn btn-link" data-toggle="modal" data-target="#CreateNewModel">
            Create new Brand
          </button>
        </td>

      </tr>
      </tbody>
    </table>

    <button class="btn p-l-0">
      <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-plus-square"
           viewBox="0 0 16 16">
        <path
          d="M14 1a1 1 0 0 1 1 1v12a1 1 0 0 1-1 1H2a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1h12zM2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2z"/>
        <path
          d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z"/>
      </svg>
    </button>

    <div class="modal fade" id="UpdateBrandModal" tabindex="-1" role="dialog" aria-labelledby="UpdateBrandModalLabel"
         aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="UpdateBrandModalLabel">Change Brand</h5>
            <button type="button" class="btn btn-close" data-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="input-group input-group-md mb-3">
              <div class="input-group-prepend">
                <span class="input-group-text" id="ChangeBrandName">Name</span>
              </div>
              <input type="text" v-model="changingBrand.Name" class="form-control js-cn" aria-label="Small"
                     aria-describedby="ChangeBrandName" :placeholder=changingBrand.Name>
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

    <div class="modal fade" id="CreateNewModel" tabindex="-1" role="dialog" aria-labelledby="CreateNewModelLabel"
         aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="CreateNewModelLabel">Change Brand</h5>
            <button type="button" class="btn btn-close" data-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="input-group input-group-md mb-3">
              <div class="input-group input-group-md mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text" id="NewBrandName">Name</span>
                </div>
                <input type="text" v-model="changingBrand.Name" class="form-control js-cn" aria-label="Small"
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
import {variables} from "../variables.js"

export default {
  name: "brand",
  data() {
    return {
      brands: [],
      changingBrand: {
        Id: 0,
        Name: "null"
      }
    }
  },
  methods: {
    refreshBrands() {
      axios.get(variables.API.BRANDS,{
        onDownloadProgress: (progressEvent) => {
          const totalLength = progressEvent.lengthComputable ? progressEvent.total : progressEvent.target.getResponseHeader('content-length') || progressEvent.target.getResponseHeader('x-decompressed-content-length');
          console.log("onUploadProgress", totalLength);
          if (totalLength !== null) {
            console.log(Math.round( (progressEvent.loaded * 100) / totalLength ));
          }
        }})
        .then((response) => {
          console.log("100");
          this.brands = response.data;
        });
    },
    UpdateBrand() {
      axios.put(variables.API.BRANDS + this.changingBrand.Id, {
        Id: this.changingBrand.Id,
        Name: this.changingBrand.Name
      }).then((response) => {
        if (response.status === 204) {
          this.refreshBrands();
        }
      });
    },
    DeleteBrand() {
      axios.delete(variables.API.BRANDS + this.changingBrand.Id)
        .then((response) => {
          if (response.status === 204) {
            this.refreshBrands();
          }
        });
    },
    CreateBrand(){
      axios.post(variables.API.BRANDS.ROOT, {
        Name: this.changingBrand.Name
      })
        .then((response) => {
          if (response.status === 201) {
            this.refreshBrands();
          }
          else{
            console.log(response);
          }
        });
    }
  },
  mounted() {
    this.refreshBrands();
  }
}
</script>
