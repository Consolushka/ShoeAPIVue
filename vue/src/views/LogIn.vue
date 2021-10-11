<template>
  <v-container style="width: 60%">
    <h1 class="text text-h3 text-center">Log In</h1>
    <dialog-account :is-login="true"></dialog-account>
  </v-container>
</template>
<script>
import DialogAccount from "../components/dialogs/dialog-account";
export default {
  components: {DialogAccount}
}
</script>