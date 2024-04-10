import axios from "axios";

export const getAllUserImages = () => {
  return axios.get(import.meta.env.VITE_GET_ALL_USERIMAGES_URL);
};
