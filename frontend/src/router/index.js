import { createRouter, createWebHashHistory } from 'vue-router';
import home from '@/components/Home.vue';
import myTasks from '@/components/MyTasks.vue';
import allTasks from '@/components/AllTasks.vue';
import login from '@/components/Login.vue';
import createTask from '@/components/CreateTask.vue';
import updateTask from '@/components/EditTask.vue';
import chat from '@/components/Chat.vue';
import authenticationService from '@/Services/authenticationService';

const routes = [
  {
    path: '/',
    name: 'home',
    component: home,
  },
  {
    path: '/myTasks',
    name: 'myTasks',
    component: myTasks,
  },
  {
    path: '/allTasks',
    name: 'allTasks',
    component: allTasks,
  },
  {
    path: '/login',
    name: 'login',
    component: login,
  },
  {
    path: '/createTask',
    name: 'createTask',
    component: createTask,
  },
  {
    path: '/updateTask/:id',
    name: 'updateTask',
    component: updateTask,
  },
  {
    path: '/chat',
    name: 'chat',
    component: chat,
  }

];

const router = createRouter({
  history: createWebHashHistory(),
  routes,
  linkActiveClass: 'active',
});

router.beforeEach(async (to) => {
  // redirect to login page if not logged in and trying to access a restricted page

  if (!authenticationService.isUserLoggedIn() && to.name !== 'login') {
    return { name: 'login' }
  }
})

export default router;
