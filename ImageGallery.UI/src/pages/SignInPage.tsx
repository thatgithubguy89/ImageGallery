import { useState } from "react";
import { signUserIn } from "../services/AuthService";
import { Link, useNavigate } from "react-router-dom";

export const SignInPage = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleSignIn = (e: { preventDefault: () => void }): any => {
    e.preventDefault();

    const data = {
      email: email,
      password: password,
    };

    signUserIn(data)
      .then((response) => {
        navigate("/");
        localStorage.setItem("username", email);
        localStorage.setItem("token", "Bearer " + response.data.accessToken);
      })
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
        <div className="mb-3">
          <Link to={"/register"}>
            <small>Not a member? Register Here</small>
          </Link>
        </div>
        <div>
          <button className="btn btn-primary" type="submit">
            Sign In
          </button>
        </div>
      </form>
    </>
  );
};
