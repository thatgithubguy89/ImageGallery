import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { Loading } from "../components/common/Loading";
import { UserImageList } from "../components/userimages/UserImageList";
import { UserImage } from "../models/UserImage";
import { getUserImagesForUser } from "../services/UserImageService";
import { Comment } from "../models/Comment";
import { getCommentsForUser } from "../services/CommentService";
import { CommentList } from "../components/comments/CommentList";

export const ProfilePage = () => {
  const [userImages, setUserImages] = useState<UserImage[]>([]);
  const [comments, setComments] = useState<Comment[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const { username } = useParams();

  useEffect(() => {
    getUserImagesForUser(username!)
      .then((response) => {
        setUserImages(response.data);
      })
      .catch((error) => console.log(error));

    getCommentsForUser(username!)
      .then((response) => {
        setComments(response.data);
        setIsLoading(false);
      })
      .catch((error) => console.log(error));
  }, []);

  if (isLoading) {
    return <Loading />;
  } else {
    return (
      <>
        <div className="container w-75">
          <ul className="nav nav-tabs" role="tablist">
            <li className="nav-item" role="presentation">
              <a
                className="nav-link"
                data-bs-toggle="tab"
                href="#home"
                aria-selected="false"
                role="tab"
                tabIndex={-1}
              >
                Images
              </a>
            </li>
            <li className="nav-item" role="presentation">
              <a
                className="nav-link active"
                data-bs-toggle="tab"
                href="#profile"
                aria-selected="true"
                role="tab"
              >
                Comments
              </a>
            </li>
          </ul>
          <div id="myTabContent" className="tab-content">
            <div
              className="container w-75 mt-5 tab-pane fade"
              id="home"
              role="tabpanel"
            >
              <UserImageList userImages={userImages} />
            </div>
            <div
              className="tab-pane fade active show"
              id="profile"
              role="tabpanel"
            >
              <CommentList
                userImageId={1}
                comments={comments}
                canAddComment={false}
              />
            </div>
          </div>
        </div>
      </>
    );
  }
};
