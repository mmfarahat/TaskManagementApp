import * as signalR from '@microsoft/signalr'
import constants from '@/helpers/constants'
import authenticationService from '@/Services/authenticationService'

let chatService = {
  connectToChatHub: function () {
    //connect to chat hub
    window.connection = new signalR.HubConnectionBuilder().withUrl(constants.Server_Base_url + '/chathub').build()
    window.connection
      .start()
      .then(async () => {
        //  let connectionid = window.connection.connectionId
        window.connection.invoke('GetConnectionId').then(async (connectionId) => {
          localStorage.setItem('connectionid', connectionId)
          await authenticationService.updateConnectionId(connectionId)
        });
      })
      .catch((err) => console.error(err.toString()))
  },
};
export default chatService;