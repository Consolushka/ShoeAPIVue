<template>
  <v-snackbar
      v-model="snackbar"
      timeout="-1"
      :max-width=statusType.curr.width
      :min-width=statusType.curr.width
      :color=statusType.curr.color
  >
    <div class="d-flex flex-column align-center" v-if="statusType.curr.isWarn">
      <p class="text-center">
        <v-icon color="yellow">mdi-alert-circle-outline</v-icon>
        Warning: you really want to delete this brand with accompanying shoes?
      </p>
      <div>
        <v-btn color="green" class="mr-5" @click="$emit('force')">
          Sure
        </v-btn>
        <v-btn color="red" @click="Close">
          Cancel
        </v-btn>
      </div>
    </div>

    <v-icon  v-if="statusType.curr.isFine" color="white">mdi-check-circle</v-icon>
    <v-icon color="white"  v-if="statusType.curr.isErr">mdi-alert</v-icon>
  </v-snackbar>
</template>

<script>
export default {
  props: {
    snackbar: Boolean,
    status: Number
  },
  data() {
    return {
      statusType: {
        curr: {
          width: 54,
          color: "none"
        },
        Fine: {
          isFine: true,
          isErr: false,
          isWarn: false,
          width: 54,
          color: "green"
        },
        Error: {
          isFine: false,
          isErr: true,
          isWarn: false,
          width: 54,
          color: "red"
        },
        Warning: {
          isFine: false,
          isErr: false,
          isWarn: true,
          width: 344,
          color: "none"
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
    switch (this.status) {
      case 409:
        this.statusType.curr = this.statusType.Warning;
        break;
      default:
        setTimeout(this.Close, 2000);
        this.statusType.curr = this.statusType.Fine;
        break;
    }
  }
}
</script>