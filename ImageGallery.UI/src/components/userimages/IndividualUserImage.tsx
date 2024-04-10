import { UserImage } from "../../models/UserImage";

interface Props {
  userImage: UserImage;
}

export const IndividualUserImage = ({ userImage }: Props) => {
  return (
    <>
      <div className="col-xl-3 col-lg-4 col-md-6 mb-4">
        <div
          className="bg-white rounded shadow-sm"
          style={{ cursor: "pointer" }}
        >
          <img
            src="https://bootstrapious.com/i/snippets/sn-gallery/img-1.jpg"
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
