<template>
  <v-form :disabled="disabled">
    <v-row>
      <v-col col="12">
        <v-text-field
            label="Name"
            v-model="brand.Name"
        ></v-text-field>
      </v-col>
    </v-row>
  </v-form>
</template>

<script>
import {Brand} from "../../utils/classes";

export default {
  props: {
    brand: Brand,
    disabled: {
      type: Boolean,
      default: false
    }
  }
}
</script>