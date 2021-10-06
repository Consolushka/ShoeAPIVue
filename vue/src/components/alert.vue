<template>
  <v-snackbar
      v-model="snackbar"
      timeout="-1"
      max-width="54"
      min-width="54"
      :color="status? 'green': 'red'"
  >

    <v-icon v-if="status" color="white">mdi-check-circle</v-icon>
    <v-icon v-else color="white">mdi-alert</v-icon>
  </v-snackbar>
</template>

<script>
export default {
  props: {
    snackbar: Boolean,
    status: Boolean
  },
  methods:{
    Close(){
      this.$emit('close');
    }
  },
  created() {
    setTimeout(this.Close, 2000);
  }
}
</script>