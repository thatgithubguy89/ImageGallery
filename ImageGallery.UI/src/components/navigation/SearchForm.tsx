import { useState } from "react";
import { Link } from "react-router-dom";

export const SearchForm = () => {
  const [searchPhrase, setSearchPhrase] = useState("");

  const handleSearch = () => {
    localStorage.setItem("search", searchPhrase);

    window.location.reload();
  };

  return (
    <>
      <div className="d-flex">
        <input
          className="form-control me-sm-2"
          type="search"
          placeholder="Search"
          onChange={(e) => setSearchPhrase(e.target.value)}
        />
        <Link
          to={"/"}
          className="btn btn-secondary my-2 my-sm-0"
          onClick={handleSearch}
        >
          Search
        </Link>
      </div>
    </>
  );
};
