<script>
import { useRoute, useRouter } from 'vue-router'
import authenticationService from './Services/authenticationService'
import { computed } from 'vue'

export default {
  name: 'App',
  // components: { Loader },
  methods: {
    async logout() {
      authenticationService.logout()
      this.$router.push('/#/login')
    }
  },
  // computed: {
  //   isUserLoggedIn() {
  //     debugger
  //     return authenticationService.isUserLoggedIn()
  //   }
  // },

  async mounted() {
    const route = useRoute()
    const router = useRouter()

    await router.isReady()

    authenticationService.getLoggedInUserName()
  },
  setup() {
    return {
      isAuthenticated: computed(() => authenticationService.isAuthenticated.value),
      loggedInUserName: computed(() => authenticationService.loggedInUserName.value),
      authenticationService
    }
  }
}
</script>

<template>
  <nav class="navbar navbar-expand-lg bg-body-tertiary">
    <div class="container-fluid">
      <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav">
          <li class="nav-item">
            <a class="nav-link active" aria-current="page" href="/#/">Home</a>
          </li>
          <li class="nav-item">
            <a class="nav-link active" aria-current="page" href="/#/myTasks">My Tasks</a>
          </li>
          <li class="nav-item">
            <a class="nav-link active" aria-current="page" href="/#/allTasks">all Tasks</a>
          </li>
          <li class="nav-item">
            <a class="nav-link active" aria-current="page" href="/#/createTask">Create Task (+)</a>
          </li>
          <li class="nav-item" v-if="isAuthenticated">
            <a class="nav-link active" aria-current="page" href="javascript:void(0)" @click="logout()">logout</a>
          </li>
        </ul>
      </div>
    </div>
    <div v-if="isAuthenticated">Welcome, {{ loggedInUserName }}</div>
  </nav>

  <main class="container text-center">
    <router-view />
  </main>
</template>

<style scoped>
</style>
