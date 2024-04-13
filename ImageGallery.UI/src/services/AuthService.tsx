import axios from "axios";

export const signUserIn = (data: object) => {
  return axios.post(import.meta.env.VITE_LOGIN_URL, data);
};

export const registerUser = (data: object) => {
  return axios.post(import.meta.env.VITE_REGISTER_URL, data);
};
