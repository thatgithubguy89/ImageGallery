import { SearchForm } from "./SearchForm";
import { SignInLinks } from "./SignInLinks";
import { SignOutLinks } from "./SignOutLinks";

export const NavBar = () => {
  const handleSearch = () => {
    localStorage.setItem("search", "");

    window.location.reload();
  };

  return (
    <>
      <nav
        className="navbar navbar-expand-lg bg-light mb-5"
        data-bs-theme="light"
      >
        <div className="container-fluid">
          <a className="navbar-brand" href="#">
            Image Gallery
          </a>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarColor03"
            aria-controls="navbarColor03"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarColor03">
            <ul className="navbar-nav me-auto">
              <li className="nav-item">
                <a className="nav-link" href="/" onClick={handleSearch}>
                  Home
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
