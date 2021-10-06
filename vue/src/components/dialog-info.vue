<template>
  <v-dialog
      v-model="dialog"
      width="500"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn
          icon
          plain
          tile
          v-bind="attrs"
          v-on="on"
      >
        <v-icon>mdi-information</v-icon>
      </v-btn>
    </template>

    <v-card>
      <v-card-title>
        <span class="text-h5">More information of Brand</span>

        <v-spacer></v-spacer>

        <v-btn
            color="black"
            icon
            @click="dialog = false"
        >
          <v-icon>mdi-close</v-icon>
        </v-btn>
      </v-card-title>

      <v-card-text>
        <v-container>
          <div class="modal-body">
            <div class="input-group input-group-md mb-3">
              <div class="input-group-prepend">
                <span class="input-group-text" id="InfoBrandId">Id</span>
              </div>
              <input type="text" v-model="brand.Id" class="form-control js-cn" aria-label="Small"
                     aria-describedby="InfoBrandId" :placeholder=brand.Id readonly>
            </div>
            <div class="input-group input-group-md mb-3">
              <div class="input-group-prepend">
                <span class="input-group-text" id="InfoBrandName">Name</span>
              </div>
              <input type="text" v-model="brand.Name" class="form-control js-cn" aria-label="Small"
                     aria-describedby="InfoBrandName" :placeholder=brand.Name readonly>
            </div>
          </div>
        </v-container>

      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script>
import {Brand} from "../utils/classes";

export default {
  name: "dialog-info",
  props: {
    brand: Brand
  },
  data() {
    return {
      dialog: false
    }
  }
}
</script>