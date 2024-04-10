import axios from "axios";
import { Comment } from "../models/Comment";

export const createcomment = (comment: Comment | undefined) => {
  return axios.post(import.meta.env.VITE_CREATE_COMMENT_URL, comment);
};
