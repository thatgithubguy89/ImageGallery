export const SignOutLinks = () => {
  const username = localStorage.getItem("username");

  const handleSearch = () => {
    localStorage.setItem("search", "");

    window.location.reload();
  };

  return (
    <>
      {!username && (
        <li className="nav-item">
          <a className="nav-link" href="/signin" onClick={handleSearch}>
            Sign In
          </a>
        </li>
      )}
    </>
  );
};
