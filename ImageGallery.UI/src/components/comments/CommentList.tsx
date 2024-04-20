import { Link } from "react-router-dom";
import { Comment } from "../../models/Comment";
import { IndividualComment } from "./IndividualComment";

interface Props {
  comments: Comment[] | undefined;
  userImageId: number | undefined;
  canAddComment: boolean;
}

export const CommentList = ({
  comments,
  userImageId,
  canAddComment,
}: Props) => {
  return (
    <>
      <div className="container w-50 mt-3 mb-3">
        {canAddComment && (
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
