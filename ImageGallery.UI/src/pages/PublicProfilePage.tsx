import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { Loading } from "../components/common/Loading";
import { UserImageList } from "../components/userimages/UserImageList";
import { UserImage } from "../models/UserImage";
import { getUserImagesForUserPublic } from "../services/UserImageService";

export const PublicProfilePage = () => {
  const [userImages, setUserImages] = useState<UserImage[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const { username } = useParams();

  useEffect(() => {
    getUserImagesForUserPublic(username!)
      .then((response) => {
        setUserImages(response.data);
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
          <UserImageList userImages={userImages} />
        </div>
      </>
    );
  }
};
