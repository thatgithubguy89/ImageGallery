import axios from "axios";

export const getAllUserImages = () => {
  return axios.get(import.meta.env.VITE_GET_ALL_USERIMAGES_URL);
};

export const getSingleUserImage = (id: String | undefined) => {
  return axios.get(`${import.meta.env.VITE_GET_SINGLE_USERIMAGE_URL}${id}`);
};
