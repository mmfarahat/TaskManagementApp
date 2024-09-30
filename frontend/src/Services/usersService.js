import axiosService from './AxiosService';
const usersService = {
  async getUsers() {
    let response = await axiosService.axiosGet('Account/All');
    return response.data;
  },
  async getConnectedUsers() {
    let response = await axiosService.axiosGet('Account/GetConnectedUsers');
    return response.data;
  }
};
export default usersService;