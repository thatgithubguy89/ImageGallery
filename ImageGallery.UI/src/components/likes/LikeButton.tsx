import { createVote } from "../../services/VoteService";

interface Props {
  likesCount: number | undefined;
  userImageId?: number;
}

export const LikeButton = ({ likesCount, userImageId }: Props) => {
  const handleCreateVote = (e: { preventDefault: () => void }): any => {
    e.preventDefault();

    const data = {
      userImageId: userImageId,
      like: true,
    };

    return createVote(data)
      .then(() => window.location.reload())
      .catch((error) => console.log(error));
  };

  return (
    <>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="16"
        height="16"
        fill="grey"
        className="bi bi-arrow-up-circle ms-2"
        viewBox="0 0 16 16"
        cursor={"pointer"}
        onClick={handleCreateVote}
      >
        <path
          fillRule="evenodd"
          d="M1 8a7 7 0 1 0 14 0A7 7 0 0 0 1 8m15 0A8 8 0 1 1 0 8a8 8 0 0 1 16 0m-7.5 3.5a.5.5 0 0 1-1 0V5.707L5.354 7.854a.5.5 0 1 1-.708-.708l3-3a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1-.708.708L8.5 5.707z"
        />
      </svg>
      <small className="me-2">{likesCount}</small>
    </>
  );
};
