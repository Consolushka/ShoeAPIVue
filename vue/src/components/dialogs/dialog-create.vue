<template>
  <v-dialog
      v-model="dialog"
      width="500"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn
          color="primary"
          outlined
          plain
          v-bind="attrs"
          v-on="on"
      >
        Create New Brand
      </v-btn>
    </template>

    <v-card>
      <v-card-title>
        <span class="text-h5">Brand Creation</span>

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
          <v-form>
            <v-row>
              <v-col col="12">
                <v-text-field
                    label="Name"
                    v-model="brand.Name"
                ></v-text-field>
              </v-col>
            </v-row>
          </v-form>
        </v-container>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
            color="blue darken-1"
            text
            @click="dialog = false"
        >
          Close
        </v-btn>
        <v-btn
            color="blue darken-1"
            text
            @click="Create"
        >
          Create
        </v-btn>
        <alert v-if="snackBar" :snackbar="snackBar" :status="responseFine" @close="snackBar=false"></alert>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import {Brand} from "../../utils/classes";
import Alert from "../alert";
import axios from "axios";
import {utils} from "../../utils/utils";
import {eventBus} from "../../main";

export default {
  components: {Alert},
  data(){
    return {
      brand: new Brand(),
      dialog: false,
      responseFine: true,
      snackBar: false
    }
  },
  methods:{
    Create(){
      console.log()
      axios.post(utils.API.BRANDS, {Name: this.brand.Name})
          .then((response) => {
            if (response.status>200 && response.status<300) {
              this.responseFine = true;
              this.snackBar = true;
              console.log(this.responseFine);
              eventBus.$emit('refreshBrands');
            } else {
              this.responseFine = false;
            }
          });
    }
  }
}
</script>