<template>
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
</template>

<script>
import {Brand} from "../../utils/classes";

export default {
  props:{
    brand: Brand
  }
}
</script>