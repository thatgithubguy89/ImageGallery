import { useNavigate } from "react-router-dom";
import { UserImage } from "../../models/UserImage";

interface Props {
  userImage: UserImage;
}

export const IndividualUserImage = ({ userImage }: Props) => {
  const navigate = useNavigate();

  const handleNavigate = () => {
    navigate(`/userimage/${userImage.id}`);
  };

  return (
    <>
      <div className="col-xl-3 col-lg-4 col-md-6 mb-4">
        <div
          className="bg-white rounded shadow-sm"
          style={{ cursor: "pointer" }}
          onClick={handleNavigate}
        >
          <img
            src={`${import.meta.env.VITE_BASE_URL}${userImage.imagePath}`}
            alt=""
            className="img-fluid card-img-top"
          />
          <div className="p-2">
            <h5>
              <p>{userImage.title}</p>
            </h5>
          </div>
        </div>
      </div>
    </>
  );
};
