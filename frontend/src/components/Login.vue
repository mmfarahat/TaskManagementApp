<template>
  <form>
    <div class="mb-3">
      <label for="exampleInputEmail1" class="form-label">Email address</label>
      <input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" />
    </div>
    <div class="mb-3">
      <label for="exampleInputPassword1" class="form-label">Password</label>
      <input type="password" class="form-control" id="exampleInputPassword1" />
    </div>

    <button type="button" @click="login()" class="btn btn-primary">Submit</button>
  </form>
</template>

<script>
import authenticationService from '@/Services/authenticationService'
import chatService from '@/Services/chatService'

export default {
  name: 'login_component',

  mounted() {},
  methods: {
    async login() {
      const email = document.getElementById('exampleInputEmail1').value
      const password = document.getElementById('exampleInputPassword1').value

      await authenticationService.login(email, password)
      chatService.connectToChatHub()
      this.$router.push('/#/')
    }
  }
}
</script>

<style>
</style>