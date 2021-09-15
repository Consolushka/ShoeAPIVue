<template>
  <h1>This is Brand Page</h1>
</template>

<script>
export default {
  name: "brand",
  data(){
    return {
      brands: []
    }
  },
  methods: {
    refreshBrands(){
      axios.get(variables.API_URL+"brand")
      .then((response)=>{
        this.brands = response.data;
      });
      console.log(this.brands);
    }
  },
  mounted(){
    this.refreshBrands();
  }
}
</script>
