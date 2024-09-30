import axiosService from './AxiosService';
const tasksService = {
  async getTasks(options, customOptions) {
    const { page, rowsPerPage } = options;
    const { title, status, user, GetMyTasksOnly } = customOptions;
    let url = `Tasks/All?PageNumber=${page}&PageSize=${rowsPerPage}`;
    if (title && title !== '') {
      url += `&Title=${title}`;
    }
    if (status && status !== '') {
      url += `&TaskStatus=${status}`;
    }
    if (user && user !== '') {
      url += `&Assignee=${user}`;
    }
    if (GetMyTasksOnly) {
      url += `&GetMyTasksOnly=${GetMyTasksOnly}`;
    }
    let response = await axiosService.axiosGet(url);
    return response.data;
  },
  async createTask(task) {
    task.priority = parseInt(task.priority);
    task.status = parseInt(task.status);

    let response = await axiosService.axiosPost('Tasks/Create', task);
    return response.data;
  },
  async getTaskById(id) {
    let response = await axiosService.axiosGet(`Tasks/${id}`);
    return response.data;
  },
  async updateTask(task) {
    task.priority = parseInt(task.priority);
    task.status = parseInt(task.status);
    let response = await axiosService.axiosPut('Tasks', task);
    return response.data;
  },
};
export default tasksService;