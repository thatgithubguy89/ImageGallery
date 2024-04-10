import { useNavigate, useParams } from "react-router-dom";
import { createcomment } from "../../services/CommentService";
import { useState } from "react";
import { Comment } from "../../models/Comment";

export const CreateCommentForm = () => {
  const { userimageid } = useParams();
  const navigate = useNavigate();
  const [content, setContent] = useState("");
  const comment: Comment = {};

  const handleCreateComment = (e: { preventDefault: () => void }): any => {
    e.preventDefault();

    comment.userImageId = Number(userimageid);
    comment.content = content;

    createcomment(comment)
      .then(() => navigate(`/userimage/${userimageid}`))
      .catch((error) => console.log(error));
  };

  return (
    <>
      <form onSubmit={handleCreateComment}>
        <textarea
          className="form-control"
          cols={30}
          rows={10}
          style={{ resize: "none" }}
          onChange={(e) => setContent(e.target.value)}
        ></textarea>
        <button type="submit" className="btn btn-primary mt-3">
          Create Comment
        </button>
      </form>
    </>
  );
};
