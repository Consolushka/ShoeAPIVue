<template>
  <v-dialog
      v-model="dialog"
      width="500"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn
          color="red"
          icon
          plain
          tile
          v-bind="attrs"
          v-on="on"
      >
        <v-icon>mdi-delete</v-icon>
      </v-btn>
    </template>

    <v-card>
      <v-card-title>
        <span class="text-h5">Delete Brand</span>

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
          <v-form disabled>
            <v-row>
              <v-col col="12">
                <v-text-field
                    label="Id"
                    :value="brand.Id"
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-col col="12">
                <v-text-field
                    label="Name"
                    :value="brand.Name"
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
            @click="Delete"
        >
          Delete
        </v-btn>
        <alert v-if="snackBar" :snackbar="snackBar" :status="responseFine" @close="snackBar=false"></alert>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import {Brand} from "../../utils/classes";
import axios from "axios";
import {utils} from "../../utils/utils";
import {eventBus} from "../../main";
import Alert from "../alert";

export default {
  name: "dialog-delete",
  components: {Alert},
  props: {
    brand: Brand
  },
  data() {
    return {
      dialog: false,
      snackBar: false,
      responseFine: true
    }
  },
  methods: {
    SendRefresh(){
      eventBus.$emit('refreshBrands');
    },
    Delete() {
      axios.delete(utils.API.BRANDS + this.brand.Id)
          .then((response) => {
            if (response.status === 204) {
              this.responseFine = true;
              this.snackBar = true;
              setTimeout(this.SendRefresh, 2000);
              console.log(this.responseFine);
            } else {
              this.responseFine = false;
            }
          });
    }
  }
}
</script>