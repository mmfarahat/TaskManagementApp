import axios from 'axios';
import { ref } from 'vue';
import VueJwtDecode from 'vue-jwt-decode';
import constants from '@/helpers/constants';
import axiosService from './axiosService';


let baseUrl = 'https://localhost:7120/';
const authenticationService = {
  isAuthenticated: ref(false),
  loggedInUserName: ref(''),
  login: async function (username, password) {
    let loginRequest = {
      email: username,
      password: password,
    };
    let response = await axios.post(baseUrl + 'Account/login', loginRequest);
    if (response.status === 200 && response.data.success) {
      localStorage.setItem('user', JSON.stringify(response.data.tokenResponse));
      this.getLoggedInUserName();
    }
  },
  logout: async function () {
    await axiosService.axiosGet('Account/RemoveConnectionId');
    localStorage.removeItem('user');
    this.isAuthenticated.value = false;
  },
  isUserLoggedIn: function () {
    try {
      this.isAuthenticated.value = localStorage.getItem('user') !== null &&
        JSON.parse(localStorage.getItem('user')).token !== null &&
        new Date(JSON.parse(localStorage.getItem('user')).expiration).getTime() > new Date().getTime();
    }
    catch {
      this.isAuthenticated.value = false;
    }

    return this.isAuthenticated.value;
  },
  getUserToken: function () {
    if (!this.isUserLoggedIn()) return '';
    return JSON.parse(localStorage.getItem('user')).token;
  },
  getLoggedInUserName: function () {
    if (!this.isUserLoggedIn()) return '';
    let decoded = VueJwtDecode.decode(this.getUserToken());
    this.loggedInUserName.value = decoded[constants.Claims_Name];
    return this.loggedInUserName.value
  },
  updateConnectionId: async function (connectionId) {
    await axiosService.axiosGet('Account/UpdateConnectionId/' + connectionId);
  },
};
export default authenticationService;
