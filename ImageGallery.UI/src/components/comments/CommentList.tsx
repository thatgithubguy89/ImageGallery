import { Link } from "react-router-dom";
import { Comment } from "../../models/Comment";
import { IndividualComment } from "./IndividualComment";

interface Props {
  comments: Comment[] | undefined;
  userImageId: number | undefined;
}

export const CommentList = ({ comments, userImageId }: Props) => {
  const username = localStorage.getItem("username");

  return (
    <>
      <div className="container w-50 mt-3 mb-3">
        {username && (
          <Link
            className="btn btn-primary mb-3"
            to={`/createcomment/${userImageId}`}
          >
            Create Comment
          </Link>
        )}
        <div className="list-group">
          {comments?.map((comment) => (
            <IndividualComment key={comment.id} comment={comment} />
          ))}
        </div>
      </div>
    </>
  );
};
