<template>
  <v-app>
    <v-app-bar
      app
      color="primary"
      dark
    >
      <div class="d-flex align-center">
        <v-img
          alt="Vuetify Logo"
          class="shrink mr-2"
          contain
          src="src/assets/logo.png"
          transition="scale-transition"
          width="40"
        />
        
        <v-img
          alt="Vuetify Name"
          class="shrink mt-1 hidden-sm-and-down"
          contain
          min-width="100"
          src="https://cdn.vuetifyjs.com/images/logos/vuetify-name-dark.png"
          width="100"
        />
      </div>

      <v-tabs
          align-with-title
      >
        <v-tabs-slider color="yellow"></v-tabs-slider>

        <v-tab to="/home">
          Home
        </v-tab>
        <v-tab to="/brand">
          Brands
        </v-tab>
        <v-tab to="/shoe">
          Shoes
        </v-tab>
      </v-tabs>
    </v-app-bar>

    <v-main>
      <router-view/>
    </v-main>
  </v-app>
</template>

<script>

export default {
  name: 'App',

  data: () => ({
    //
  }),
};
</script>
