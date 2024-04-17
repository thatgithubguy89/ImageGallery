import axios from "axios";

const token = localStorage.getItem("token");

export const createVote = (data: object) => {
  return axios.post(import.meta.env.VITE_CREATE_VOTE_URL, data, {
    headers: {
      Authorization: token,
    },
  });
};
