<template>
  <section>
    <v-container>
      <h1 class="text-center text-h3">This is Shoes Page</h1>
      <h2 class="text-center text-h4">Here you can change shoes</h2>
      <v-form v-model="valid">
        <v-container>
          <v-row align="center">
            <v-col col="12" md="12" class="text-left text-h5">Filters</v-col>
            <v-col
                cols="12"
                md="3"
            >
              <v-text-field
                  v-model="filterParam.Name"
                  label="Name"
              ></v-text-field>
            </v-col>

            <v-col
                cols="12"
                md="3"
            >
              <v-select
                  :hint="`${mockBrand.Id}, ${mockBrand.Name}`"
                  :items="brands"
                  v-model="filterParam.BrandId"
                  item-text="Name"
                  item-value="Id"
                  label="Standard"
              ></v-select>
            </v-col>

            <v-col
                cols="12"
                md="3"
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
                        v-model="filterParam.CreationDate"
                        label="Creation Date"
                        prepend-icon="mdi-calendar"
                        readonly
                        v-bind="attrs"
                        v-on="on"
                    ></v-text-field>
                  </template>
                  <v-date-picker
                      v-model="filterParam.CreationDate"
                      range
                      :active-picker.sync="activePicker"
                      :max="(new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10)"
                      min="1950-01-01"
                      @change="save"
                  ></v-date-picker>
                </v-menu>
              </div>
            </v-col>

            <v-col cols="3">
              <v-row>
                <v-col cols="6">
                  <v-btn block color="primary" @click="RenderFilters">Filter</v-btn>
                </v-col>
                <v-col cols="6">
                  <v-btn block color="grey lighten-1" @click="RefreshFilters">Clear</v-btn>
                </v-col>
              </v-row>
            </v-col>
          </v-row>
        </v-container>
      </v-form>
      <dialog-create :model="mockShoe" :brands="brands" v-if="this.$store.getters.IS_ADMIN"></dialog-create>
      <v-row>
          <shoe v-for="shoe in $store.getters.SHOES" :key="shoe.Id" v-bind:shoe="shoe" :brands="brands"></shoe>
      </v-row>
    </v-container>
  </section>
</template>

<script>
import {utils} from "../utils/utils.js"
import {Brand, Shoe} from "../utils/classes.js";
import axios from 'axios'
import shoe from "../components/Shoe.vue"
import DialogCreate from "../components/dialogs/dialog-create";
import {eventBus} from "../main";

export default {
  name: "shoe-page",
  data() {
    return {
      Shoes: [],
      mockShoe: new Shoe(),
      mockBrand: {Id: 0, Name: ""},
      activePicker: null,
      date: null,
      menu: false,
      brands: [],
      filterParam: {
        BrandId: null,
        Name: null,
        CreationDate: null
      },
      valid: false,
    }
  },
  components: {
    DialogCreate,
    shoe
  },
  methods: {
    Refresh() {
      this.$store.dispatch('UPDATE_SHOES');
    },
    save (date) {
      this.$refs.menu.save(date)
    },
    RenderFilters() {
       this.$store.getters.SHOES.forEach((shoe) => {
        shoe.MatchFilter(this.filterParam);
      });
    },
    RefreshFilters(){
      this.filterParam = {
        BrandId: null,
        Name: null,
        CreationDate: null
      };
      this.RenderFilters();
    }
  },
  mounted() {
    this.Refresh();
    axios.get(utils.API.BRANDS+"GetAll")
        .then((response) => {
          this.brands = [];
          response.data.forEach((brand) => {
            this.brands.push(new Brand(brand));
          });
          this.brands.push(new Brand());
        });
  },
  created(){
    eventBus.$on('refreshShoes', ()=>{
      this.Refresh();
    })
  }
}
</script>
