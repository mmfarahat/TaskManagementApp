<template>
  <h1>my tasks</h1>
  <div class="row">
    <div class="col-3">Filters by Title: <input type="text" v-model="customOptions.title" /></div>
    <div class="col-3">
      Filter by Status:
      <select v-model="customOptions.status">
        <option value="">All</option>
        <option value="0">Open</option>
        <option value="1">In Progress</option>
        <option value="2">Closed</option>
      </select>
    </div>
  </div>

  <EasyDataTable v-model:server-options="serverOptions" :server-items-length="serverItemsLength" :loading="loading" :headers="headers" :items="items">
    <EasyDataTable v-model:server-options="serverOptions" :server-items-length="serverItemsLength" :loading="loading" :headers="headers" :items="items">
      <template #item-operation="item">
        <div class="operation-wrapper">
          <img src="https://cdn-icons-png.flaticon.com/512/2891/2891436.png" width="30" class="operation-icon" @click="editItem(item)" />
        </div>
      </template>
    </EasyDataTable>
  </EasyDataTable>
</template>
  
  <script>
import { defineComponent, ref, computed, watch } from 'vue'
import Vue3EasyDataTable from 'vue3-easy-data-table'
import tasksService from '@/Services/tasksService'
import usersService from '@/Services/usersService'

export default {
  name: 'my_tasks',

  mounted() {
    //load users
  },

  setup() {
    const headers = [
      { text: 'title', value: 'title' },
      { text: 'description', value: 'description' },
      { text: 'dueDate', value: 'dueDate' },
      { text: 'priority', value: 'priorityString' },
      { text: 'status', value: 'statusString' },
      { text: 'Assigned To', value: 'assignedToName' },
      { text: 'Operation', value: 'operation' }
    ]
    const items = ref([])
    const users = ref([])

    const serverItemsLength = ref(0)
    const serverOptions = ref({
      page: 1,
      rowsPerPage: 5
    })
    const customOptions = ref({
      status: null,
      title: '',
      GetMyTasksOnly: true
    })

    const loading = ref(false)

    const loadFromServer = async (options) => {
      loading.value = true
      const { tasks, totalCount } = await tasksService.getTasks(serverOptions.value, customOptions.value)
      items.value = tasks
      serverItemsLength.value = totalCount
      loading.value = false
    }

    // first load when created
    loadFromServer()

    watch(
      serverOptions,
      (value, old) => {
        loadFromServer()
      },
      { deep: true }
    )

    watch(
      customOptions,
      (value, old) => {
        loadFromServer()
      },
      { deep: true }
    )

    const loadUsers = async () => {
      users.value = await usersService.getUsers()
    }
    loadUsers()
    return {
      headers,
      items,
      serverOptions,
      customOptions,
      serverItemsLength,
      loading,
      loadFromServer,
      users
    }
  }
}
</script>
  