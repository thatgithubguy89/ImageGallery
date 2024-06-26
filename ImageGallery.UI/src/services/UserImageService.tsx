import axios from "axios";
import { QueryRequest } from "../models/QueryRequest";

const token = localStorage.getItem("token");

export const createUserImage = (data: object) => {
  return axios.post(import.meta.env.VITE_CREATE_USERIMAGE_URL, data, {
    headers: {
      "Content-Type": "multipart/form-data",
      Authorization: token,
    },
  });
};

export const deleteUserImage = (id: String | undefined) => {
  return axios.delete(`${import.meta.env.VITE_DELETE_USERIMAGE_URL}${id}`, {
    headers: {
      Authorization: token,
    },
  });
};

export const getAllUserImages = (request: QueryRequest) => {
  return axios.post(import.meta.env.VITE_GET_ALL_USERIMAGES_URL, request);
};

export const getSingleUserImage = (id: String | undefined) => {
  return axios.get(`${import.meta.env.VITE_GET_SINGLE_USERIMAGE_URL}${id}`);
};

export const getUserImagesForUser = (username: String) => {
  return axios.get(
    `${import.meta.env.VITE_GET_USER_IMAGES_FOR_USER_URL}${username}`
  );
};
