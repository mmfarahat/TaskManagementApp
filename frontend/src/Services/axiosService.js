import axios from 'axios';
import authenticationService from './authenticationService';
import { createRouter, createWebHashHistory } from 'vue-router';
import { useToast } from 'vue-toast-notification';

let baseUrl = 'https://localhost:7120/';

const axiosService = {
  refreshSubscribers: [],
  axiosPost: async function (url, params, config) {

    let axiosInstance = axios.create({
      baseURL: baseUrl,
    });
    this.addRequestInterceptor(axiosInstance, config);
    this.addResponseInterceptor(axiosInstance);

    let res = await axiosInstance.post(url, params, config);
    return res;
  },
  axiosGet: async function (url, config) {
    let axiosInstance = axios.create({
      baseURL: baseUrl,
    });

    this.addRequestInterceptor(axiosInstance, config);
    this.addResponseInterceptor(axiosInstance);

    if (config) {
      let res = await axiosInstance.get(url, config);
      return res;
    } else {
      let res = await axiosInstance.get(url);
      return res;
    }
  },
  axiosPut: async function (url, params, config) {
    let axiosInstance = axios.create({
      baseURL: baseUrl,
    });
    this.addRequestInterceptor(axiosInstance, config);
    this.addResponseInterceptor(axiosInstance);

    let res = await axiosInstance.put(url, params);

    return res;
  },
  addRequestInterceptor: function (axiosInstance, config) {
    // Add a request interceptor

    axiosInstance.interceptors.request.use(
      async function (axiosConfig) {
        var userToken = authenticationService.getUserToken();

        if (config && config.headers) axiosConfig.headers = config.headers;
        else {
          axiosConfig.headers = {
            Authorization: `Bearer ${userToken}`,
          };
        }
        return axiosConfig;
      },
      function (error) {

        return Promise.reject(error);
      }
    );
  },
  addResponseInterceptor: function (axiosInstance) {
    var self = this;
    axiosInstance.interceptors.response.use(
      (response) => {
        return response;
      },
      function (error) {
        const originalRequest = error.config;
        //  document.getElementById('loadingoverlay').style.display = 'none';
        if (error.response) {
          const $toast = useToast();
          if (error.response.data.title) {
            let instance = $toast.error(error.response.data.title, { position: 'top' });
          }
          else if (error.response.data) {
            let errors = error.response.data;
            let errorString = '';
            for (let key in errors) {
              errorString += errors[key] + '<br>';
            }
            let instance = $toast.error(errorString, { position: 'top' });

          }
        }
        if (error.response && error.response.status === 401) {
          if (!authenticationService.isUserLoggedIn()) {
            this.$router.push('/#/login');
          }

        } else {
          return Promise.reject(error);
        }
      }
    );
  },
  onRefreshed: function (token) {
    this.refreshSubscribers.map((cb) => cb(token));
  },
  subscribeTokenRefresh: function (cb) {
    this.refreshSubscribers.push(cb);
  }
};
export default axiosService;
