import { resetSearchPhrase } from "../../services/UtilityService";

export const SignOutLinks = () => {
  const username = localStorage.getItem("username");

  return (
    <>
      {!username && (
        <li className="nav-item">
          <a className="nav-link" href="/signin" onClick={resetSearchPhrase}>
            Sign In
          </a>
        </li>
      )}
    </>
  );
};
