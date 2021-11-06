<template>
  <div>
    <div>
      <h3 class="text-h3" v-if="isFine === null">Wait...</h3>
    </div>
    <div v-if="isFine">
      <h3 class="text-h3">Your Account is now confirmed</h3>
      <h3 class="text-h4">You can close this page</h3>
    </div>
    <div v-if="isFine===false">
      <h3 class="text-h3">Something gone wrong</h3>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import {utils} from "../utils/utils";

export default {
  data(){
    return{
      isFine: null
    }
},
  mounted() {
    let url = window.location.href;
    let keyParam =url.split('?')[1];
    let stringKey = keyParam.substring(4,keyParam.length);
    console.log(stringKey);
    axios.post(utils.API.USER+`ConfirmRegistration?key=${stringKey}`)
    .then(()=>{
      this.isFine = true;
    })
    .catch(()=>{
      this.isFine = false;
    });
  }
}
</script>