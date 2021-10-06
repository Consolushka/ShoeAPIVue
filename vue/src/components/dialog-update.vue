<template>
    <v-dialog
        v-model="dialog"
        width="500px"
    >
      <template v-slot:activator="{ on, attrs }">
        <v-btn
            icon
            plain
            tile
            color="primary"
            v-bind="attrs"
            v-on="on"
        >
          <v-icon>mdi-pencil-outline</v-icon>
        </v-btn>
      </template>
      <v-card>
        <v-card-title>
          <span class="text-h5">Update Brand</span>

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
            <v-row>
              <v-col
                  cols="12"
              >
                <v-text-field
                    label="Brand Name"
                    v-model="brand.Name"
                >{{brand.Name}}</v-text-field>
              </v-col>
            </v-row>
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
              @click="dialog = false"
          >
            Save
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
</template>

<script>
import {Brand} from "../utils/classes";

export default {
  name: "dialog-update",
  props:{
    brand: Brand
  },
  data: () => ({
    dialog: false,
  }),
}
</script>