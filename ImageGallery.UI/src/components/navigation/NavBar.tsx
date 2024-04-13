export const NavBar = () => {
  const username = localStorage.getItem("username");

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
                <a className="nav-link" href="/">
                  Home
                </a>
              </li>
              {!username && (
                <li className="nav-item">
                  <a className="nav-link" href="/signin">
                    Sign In
                  </a>
                </li>
              )}
              {username && (
                <li className="nav-item">
                  <a className="nav-link" href="/">
                    Sign Out
                  </a>
                </li>
              )}
              {username && (
                <li className="nav-item">
                  <a className="nav-link" href="profile">
                    <i className="bi bi-person"></i>
                  </a>
                </li>
              )}
            </ul>
            <form className="d-flex">
              <input
                className="form-control me-sm-2"
                type="search"
                placeholder="Search"
              />
              <button className="btn btn-secondary my-2 my-sm-0" type="submit">
                Search
              </button>
            </form>
          </div>
        </div>
      </nav>
    </>
  );
};
