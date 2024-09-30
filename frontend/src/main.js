import * as bootstrap from 'bootstrap/dist/js/bootstrap.bundle';
import './assets/main.css'
import router from './router';
import { createApp } from 'vue'
import App from './App.vue'
import Vue3EasyDataTable from "vue3-easy-data-table";
import "vue3-easy-data-table/dist/style.css";
import ToastPlugin from 'vue-toast-notification';
import 'vue-toast-notification/dist/theme-bootstrap.css';



createApp(App)
  .component("EasyDataTable", Vue3EasyDataTable)
  .provide('bootstrap', bootstrap)
  .use(router)
  .use(ToastPlugin, { position: 'top' })
  .mount('#app');
