import { useNavigate, useParams } from "react-router-dom";
import { createcomment } from "../../services/CommentService";
import { useState } from "react";

export const CreateCommentForm = () => {
  const { userimageid } = useParams();
  const navigate = useNavigate();
  const [content, setContent] = useState("");

  const handleCreateComment = (e: { preventDefault: () => void }): any => {
    e.preventDefault();

    const data = {
      userImageId: Number(userimageid),
      content: content,
    };

    createcomment(data)
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
