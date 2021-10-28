<template>
  <v-snackbar
      v-model="snackbar"
      timeout="-1"
      :color="statuses[status].color"
  >
    <div class="d-flex align-center">
      <v-icon color="white">mdi-check-circle</v-icon>
      <div class="flex-grow-1 d-flex justify-center align-center">
        <p class="mb-0">{{ text }}</p>
      </div>
    </div>
  </v-snackbar>
</template>

<script>
export default {
  props: {
    snackbar: Boolean,
    status: String,
    text: String
  },
  data() {
    return {
      statuses:{
        "Fine": {
          color: "green"
        },
        "Error":{
          color: "red"
        }
      }
    }
  },
  methods: {
    Close() {
      this.$emit('close');
    }
  },
  created() {
    setTimeout(this.Close, 3000);
  }
}
</script>