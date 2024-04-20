import axios from "axios";
import { Comment } from "../models/Comment";

const token = localStorage.getItem("token");

export const createcomment = (comment: Comment | undefined) => {
  return axios.post(import.meta.env.VITE_CREATE_COMMENT_URL, comment, {
    headers: {
      Authorization: token,
    },
  });
};

export const getCommentsForUser = (username: String) => {
  return axios.get(
    `${import.meta.env.VITE_GET_COMMENTS_FOR_USER_URL}${username}`
  );
};
