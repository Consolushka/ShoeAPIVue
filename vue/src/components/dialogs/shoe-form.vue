<template>
  <v-form v-model="valid">
    <v-container>
      <v-row align="center">
        <v-col cols="7">
          <v-row align="end">
            <v-col
                cols="12"
            >
              <v-text-field
                  v-model="shoe.Name"
                  label="Name"
              ></v-text-field>
            </v-col>

            <v-col
                cols="12"
            >
              <v-select
                  v-model="shoe.Brand.Id"
                  :hint="`${selectedBrand.Id}, ${selectedBrand.Name}`"
                  :items="brands"
                  item-text="Name"
                  item-value="Id"
                  label="Standard"
              ></v-select>
            </v-col>

            <v-col
                cols="12"
            >
              <div>
                <v-menu
                    ref="menu"
                    v-model="menu"
                    :close-on-content-click="false"
                    transition="scale-transition"
                    offset-y
                    min-width="auto"
                >
                  <template v-slot:activator="{ on, attrs }">
                    <v-text-field
                        v-model="shoe.CreationTime"
                        label="Birthday date"
                        prepend-icon="mdi-calendar"
                        readonly
                        v-bind="attrs"
                        v-on="on"
                    ></v-text-field>
                  </template>
                  <v-date-picker
                      v-model="shoe.CreationTime"
                      :active-picker.sync="activePicker"
                      :max="(new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10)"
                      min="1950-01-01"
                      @change="save"
                  ></v-date-picker>
                </v-menu>
              </div>
            </v-col>
          </v-row>
        </v-col>
        <v-col cols="5">
         <v-img
             :src="`${photoUrl}${shoe.PhotoFileName}`"
             cover
         >

         </v-img>
          <input type="file" v-on:change="UploadImage">
        </v-col>
      </v-row>

    </v-container>
  </v-form>
</template>

<script>
import {Shoe} from "../../utils/classes";
import {Brand} from "../../utils/classes";
import {utils} from "../../utils/utils";
import axios from "axios";

export default {
  props: {
    shoe: Shoe,
    brands: Array
  },
  data() {
    return {
      selectedBrand: new Brand(),
      activePicker: null,
      date: null,
      menu: false,
      valid: true,
      photoUrl: utils.PHOTO_URL,
    }
  },
  methods: {
    save(date) {
      this.$refs.menu.save(date)
    },
    UploadImage(e) {
      let form = new FormData();
      console.log(e.target.files);
      form.append("file", e.target.files[0]);
      console.log(form);
      console.log(`${utils.API.SHOES}SaveFile`);
      axios.post(`${utils.API.SHOES}SaveFile`, form)
          .then((response) => {
            console.log(response);
            this.shoe.PhotoFileName = response.data;
          });
    },
  },
  created() {
    console.log(this.shoe.PhotoFileName);
  }
}
</script>