<template>
  <v-container style="width: 60%">
    <h1 class="text text-h3 text-center">Sign Up</h1>
    <dialog-account :is-login="false"></dialog-account>
  </v-container>
</template>
<script>
import DialogAccount from "../components/dialogs/dialog-account";
export default {
  components: {DialogAccount}
}
</script>