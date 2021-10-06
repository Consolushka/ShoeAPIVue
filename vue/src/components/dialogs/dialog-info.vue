<template>
  <v-dialog
      v-model="dialog"
      width="500"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn
          color="green"
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
    </v-card>
  </v-dialog>
</template>

<script>
import {Brand} from "../../utils/classes";

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