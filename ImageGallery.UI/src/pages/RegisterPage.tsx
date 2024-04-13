import { useState } from "react";
import { registerUser } from "../services/AuthService";
import { useNavigate } from "react-router-dom";

export const RegisterPage = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleSignIn = (e: { preventDefault: () => void }): any => {
    e.preventDefault();

    const data = {
      email: email,
      password: password,
    };

    registerUser(data)
      .then(() => navigate("/signin"))
      .catch((error) => console.log(error));
  };

  return (
    <>
      <form className="container w-25" onSubmit={handleSignIn}>
        <div>
          <input
            type="email"
            className="form-control mb-3"
            placeholder="Email"
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>
        <div>
          <input
            type="password"
            className="form-control mb-3"
            placeholder="Password"
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        <div>
          <button className="btn btn-primary" type="submit">
            Register
          </button>
        </div>
      </form>
    </>
  );
};
