import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { UserImage } from "../models/UserImage";
import {
  deleteUserImage,
  getSingleUserImage,
} from "../services/UserImageService";
import { CommentList } from "../components/comments/CommentList";
import { DislikeButton } from "../components/likes/DislikeButton";
import { LikeButton } from "../components/likes/LikeButton";
import { Loading } from "../components/common/Loading";

export const UserImagePage = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const username = localStorage.getItem("username");
  const [userImage, setUserImage] = useState<UserImage>();
  const [isLoading, setIsLoading] = useState(true);

  const handleDeleteUserImage = () => {
    return deleteUserImage(id)
      .then(() => navigate("/"))
      .catch((error) => console.log(error));
  };

  useEffect(() => {
    getSingleUserImage(id)
      .then((response) => {
        setUserImage(response.data);
        setIsLoading(false);
      })
      .catch((error) => console.log(error));
  }, []);

  if (isLoading) {
    return <Loading />;
  } else {
    return (
      <>
        <div className="container">
          <div className="card mb-3">
            <h6 className="card-header">{userImage?.title}</h6>
            <img
              src={`${import.meta.env.VITE_BASE_URL}${userImage?.imagePath}`}
              alt=""
              className="img-fluid card-img-top"
              style={{ width: "100%", height: "500px" }}
            />
            <div className="card-footer text-muted">
              <LikeButton
                userImageId={userImage?.id}
                likesCount={userImage?.likesCount}
              />
              <DislikeButton
                userImageId={userImage?.id}
                dislikesCount={userImage?.dislikesCount}
              />
              <small className="mb-1">
                Created By{" "}
                <Link to={`/profile/${userImage?.username}`}>
                  {userImage?.username}
                </Link>{" "}
                on {userImage?.createTime?.toString().substring(0, 10)}
              </small>{" "}
              {username == userImage?.username && (
                <i
                  className="bi bi-trash"
                  style={{ cursor: "pointer" }}
                  onClick={handleDeleteUserImage}
                ></i>
              )}
            </div>
          </div>
        </div>

        <CommentList
          comments={userImage?.comments}
          userImageId={userImage?.id}
          canAddComment={true}
        />
      </>
    );
  }
};
