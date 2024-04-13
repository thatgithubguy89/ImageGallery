import { useState } from "react";
import { createUserImage } from "../services/UserImageService";
import { useNavigate } from "react-router-dom";

export const CreateUserImagePage = () => {
  const [file, setFile] = useState<Blob>(new Blob());
  const [title, setTitle] = useState("");
  const navigate = useNavigate();

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    let file: any = event.currentTarget.files;
    setFile(file[0]);
  };

  const handleCreateUserImage = (e: { preventDefault: () => void }): any => {
    e.preventDefault();

    const data = {
      userImageDto: {
        title: title,
      },
      file: file,
    };

    createUserImage(data)
      .then(() => navigate("/profile"))
      .catch((error) => console.log(error));
  };

  return (
    <>
      <form className="container w-25" onSubmit={handleCreateUserImage}>
        <div>
          <input
            type="text"
            className="form-control mb-3"
            placeholder="Title"
            onChange={(e) => setTitle(e.target.value)}
          />
        </div>
        <div>
          <input
            className="form-control mb-3"
            type="file"
            onChange={handleFileChange}
          />
        </div>
        <div>
          <button className="btn btn-primary" type="submit">
            Create Image
          </button>
        </div>
      </form>
    </>
  );
};
