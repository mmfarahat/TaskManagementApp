<template>
  <h1>create task</h1>
  <form>
    <div class="mb-3">
      <label for="exampleInputEmail1" class="form-label">Title</label>
      <input type="email" class="form-control" id="tasktitle" v-model="createTaskCommand.title" />
    </div>
    <div class="mb-3">
      <label for="exampleInputPassword1" class="form-label">Description</label>
      <textarea class="form-control" id="taskDescription" v-model="createTaskCommand.description"></textarea>
    </div>

    <div class="mb-3">
      <label for="exampleInputPassword1" class="form-label">Task Due Date</label>
      <input type="date" class="form-control" id="taskDueDate" v-model="createTaskCommand.dueDate" />
    </div>

    <div class="mb-3">
      <label for="exampleInputPassword1" class="form-label">Task Priority</label>
      <select type="date" class="form-control" id="taskPriority" v-model="createTaskCommand.priority">
        <option value="0">Low</option>
        <option value="1">Medium</option>
        <option value="2">High</option>
      </select>
    </div>
    <div class="mb-3">
      <label for="exampleInputPassword1" class="form-label">Task Status</label>
      <select type="date" class="form-control" id="taskPriority" v-model="createTaskCommand.status">
        <option value="0">Open</option>
        <option value="1">In Progress</option>
        <option value="2">Closed</option>
      </select>
    </div>
    <div class="mb-3">
      <label for="exampleInputPassword1" class="form-label">Assigned to</label>
      <select v-model="createTaskCommand.assignedTo" class="form-select">
        <option v-for="user in users" :value="user.id" :key="user.id">{{ user.email }}</option>
      </select>
    </div>

    <button type="button" @click="createTask()" class="btn btn-primary">Submit</button>
  </form>
</template>
  
  <script>
import tasksService from '@/Services/tasksService'
import usersService from '@/Services/usersService'
import { ref } from 'vue'
import { useToast } from 'vue-toast-notification'

export default {
  name: 'create_task',

  mounted() {
    //load users
  },
  methods: {
    async createTask() {
      await tasksService.createTask(this.createTaskCommand)
      const $toast = useToast()
      let instance = $toast.success('task created', { position: 'top' })
      this.$router.push('/allTasks')
    }
  },
  setup() {
    const createTaskCommand = ref({
      title: '',
      description: '',
      dueDate: '',
      priority: 0,
      status: 0,
      assignedTo: ''
    })
    const users = ref([])
    const loadUsers = async () => {
      users.value = await usersService.getUsers()
    }
    loadUsers()
    return { createTaskCommand, users }
  }
}
</script>
  