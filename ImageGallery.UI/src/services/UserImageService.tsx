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

export const getUserImagesForUser = (username: String) => {
  return axios.get(
    `${import.meta.env.VITE_GET_USER_IMAGES_FOR_USER_URL}${username}`
  );
};
