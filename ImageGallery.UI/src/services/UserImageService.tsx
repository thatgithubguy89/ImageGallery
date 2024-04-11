import axios from "axios";

export const createUserImage = (data: object) => {
  return axios.post(import.meta.env.VITE_CREATE_USERIMAGE_URL, data, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
};

export const getAllUserImages = () => {
  return axios.get(import.meta.env.VITE_GET_ALL_USERIMAGES_URL);
};

export const getSingleUserImage = (id: String | undefined) => {
  return axios.get(`${import.meta.env.VITE_GET_SINGLE_USERIMAGE_URL}${id}`);
};
