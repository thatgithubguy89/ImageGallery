import { useEffect, useState } from "react";
import { getUserImagesForUser } from "../services/UserImageService";
import { UserImage } from "../models/UserImage";
import { UserImageList } from "../components/userimages/UserImageList";
import { Link, useNavigate } from "react-router-dom";

export const ProfilePage = () => {
  const [userImages, setUserImages] = useState<UserImage[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    const username: string | any = localStorage.getItem("username");
    if (!username) {
      navigate("/");
    }

    getUserImagesForUser(username)
      .then((response) => setUserImages(response.data))
      .catch((error) => console.log(error));
  }, []);

  return (
    <>
      <div className="container w-75">
        <Link to={"/createuserimage"} className="btn btn-primary mb-3 ms-5">
          Create Image
        </Link>
        <UserImageList userImages={userImages} />
      </div>
    </>
  );
};
