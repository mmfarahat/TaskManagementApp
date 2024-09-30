import axiosService from './AxiosService';
const usersService = {
  async getUsers() {
    let response = await axiosService.axiosGet('Account/All');
    return response.data;
  }
};
export default usersService;