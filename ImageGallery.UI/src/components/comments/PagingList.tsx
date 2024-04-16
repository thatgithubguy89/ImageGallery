import { useEffect, useState } from "react";

interface Props {
  pageTotal: number;
  currentPage: number;
  handleGetItems: (page: number) => void;
}

export const PagingList = ({
  pageTotal,
  currentPage,
  handleGetItems,
}: Props) => {
  const [pageArray, setPageArray] = useState<number[]>([]);

  useEffect(() => {
    const newArr = handlePageArray(pageTotal);
    setPageArray(newArr);
  }, []);

  // Create an array that will hold 1 - pageTotal
  const handlePageArray = (pageTotal: number) => {
    const pageArray = [];

    for (let index = 1; index <= pageTotal; index++) {
      pageArray.push(index);
    }

    return pageArray;
  };

  return (
    <div>
      <ul className="pagination d-flex justify-content-center mt-3">
        {pageArray.map((page, index) => (
          <li
            key={index}
            className={
              // Make the current page active
              currentPage == index + 1 ? "page-item active" : "page-item"
            }
          >
            <a
              onClick={() => {
                handleGetItems(page);
              }}
              className="page-link"
              href="#"
            >
              {page}
            </a>
          </li>
        ))}
      </ul>
    </div>
  );
};
