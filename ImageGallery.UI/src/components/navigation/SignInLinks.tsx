import { Link } from "react-router-dom";

export const SignInLinks = () => {
  const username = localStorage.getItem("username");

  const handleSignOut = () => {
    localStorage.setItem("username", "");
    localStorage.setItem("token", "");
    localStorage.setItem("search", "");

    window.location.reload();
  };

  return (
    <>
      {username && (
        <li className="nav-item">
          <Link to={"/"} className="nav-link" onClick={handleSignOut}>
            Sign Out
          </Link>
        </li>
      )}
      {username && (
        <li className="nav-item">
          <a className="nav-link" href="profile">
            <i className="bi bi-person"></i>
          </a>
        </li>
      )}
    </>
  );
};
