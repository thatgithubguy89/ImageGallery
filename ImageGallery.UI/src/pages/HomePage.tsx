import { useEffect, useState } from "react";
import { UserImage } from "../models/UserImage";
import { UserImageList } from "../components/userimages/UserImageList";
import { getAllUserImages } from "../services/UserImageService";

export const HomePage = () => {
  const [userImages, setUserImages] = useState<UserImage[]>([]);

  useEffect(() => {
    getAllUserImages()
      .then((response) => setUserImages(response.data))
      .catch((error) => console.log(error));
  }, []);

  return (
    <>
      <UserImageList userImages={userImages} />
    </>
  );
};
