<template>
  <h1>chat</h1>

  <div>
    <div>
      active users
      <ul>
        <li v-for="user in users" :key="user.userId" :value="user.connectionId">
          <a @click="startChat()" href="javascript:void(0)">{{ user.email }} </a>
        </li>
      </ul>
    </div>
  </div>
  <div id="chatWidget" v-if="showChat">
    <input v-model="message" placeholder="Message" />
    <button @click="sendMessage">Send</button>
    <ul>
      <li v-for="msg in messages" :key="msg.id">{{ msg.user }}: {{ msg.message }}</li>
    </ul>
  </div>
</template>

<script>
import * as signalR from '@microsoft/signalr'
import constants from '@/helpers/constants'
import authenticationService from '@/Services/authenticationService'
import usersService from '@/Services/usersService'
import chatService from '@/Services/chatService'

import { ref } from 'vue'
export default {
  name: 'chat_component',
  setup() {
    const users = ref([])
    const showChat = ref(false)
    const recevierId = ref('')

    let getConnectedUsers = async () => {
      let usersResponse = await usersService.getConnectedUsers()
      users.value = usersResponse
    }
    getConnectedUsers()
    return {
      users,
      showChat,
      recevierId
    }
  },
  data() {
    return {
      user: '',
      message: '',
      messages: []
    }
  },
  created() {
    //  this.connection = new signalR.HubConnectionBuilder().withUrl(constants.Server_Base_url + '/chathub').build()
    if (window.connection == undefined || window.connection == null) {
      chatService.connectToChatHub()

      window.connection.on('ReceiveMessage', (senderId, message) => {
        //    alert('message received')
        alert(message)
        //  this.recevierId = senderId
        this.messages.push({ message })
      })
    } else {
      window.connection.on('ReceiveMessage', (senderId, message) => {
        // alert('message received')
        alert(message)
        //  this.recevierId = senderId
        //  this.messages.push({ message })
      })
    }
  },
  methods: {
    sendMessage() {
      window.connection.invoke('SendMessage', this.recevierId, this.message).catch((err) => console.error(err.toString()))
      this.message = ''
    },
    startChat() {
      this.showChat = true
      this.recevierId = event.target.parentNode.getAttribute('value')
    }
  }
}
</script>
