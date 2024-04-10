import { UserImage } from "../../models/UserImage";
import { IndividualUserImage } from "./IndividualUserImage";

interface Props {
  userImages: UserImage[];
}

export const UserImageList = ({ userImages }: Props) => {
  return (
    <>
      <div className="container-fluid">
        <div className="px-lg-5">
          <div className="row">
            {userImages.map((userImage) => (
              <IndividualUserImage key={userImage.id} userImage={userImage} />
            ))}
          </div>
        </div>
      </div>
    </>
  );
};
