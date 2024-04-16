import { SearchForm } from "./SearchForm";
import { SignInLinks } from "./SignInLinks";
import { SignOutLinks } from "./SignOutLinks";
import { resetSearchPhrase } from "../../services/UtilityService";

export const NavBar = () => {
  return (
    <>
      <nav
        className="navbar navbar-expand-lg bg-light mb-5"
        data-bs-theme="light"
      >
        <div className="container-fluid">
          <div className="collapse navbar-collapse" id="navbarColor03">
            <ul className="navbar-nav me-auto">
              <li className="nav-item">
                <a className="nav-link" href="/" onClick={resetSearchPhrase}>
                  Image Gallery
                </a>
              </li>
              <SignInLinks />
              <SignOutLinks />
            </ul>
            <SearchForm />
          </div>
        </div>
      </nav>
    </>
  );
};
