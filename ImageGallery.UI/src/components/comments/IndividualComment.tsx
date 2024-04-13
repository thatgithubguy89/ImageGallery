import { Link } from "react-router-dom";
import { Comment } from "../../models/Comment";

interface Props {
  comment: Comment | undefined;
}

export const IndividualComment = ({ comment }: Props) => {
  return (
    <>
      <div className="list-group-item list-group-item-action flex-column align-items-start p-4">
        <div className="d-flex w-100 justify-content-between">
          <small className="mb-1">
            Created By <Link to={"/"}>{comment?.username}</Link> on{" "}
            {comment?.createTime?.toString().substring(0, 10)}
          </small>
        </div>
        <p className="mb-1">{comment?.content}</p>
      </div>
    </>
  );
};
