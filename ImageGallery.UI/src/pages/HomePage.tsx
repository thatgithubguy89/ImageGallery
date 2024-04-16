import { useEffect, useState } from "react";
import { UserImage } from "../models/UserImage";
import { UserImageList } from "../components/userimages/UserImageList";
import { getAllUserImages } from "../services/UserImageService";
import { QueryRequest } from "../models/QueryRequest";
import { PagingList } from "../components/comments/PagingList";
import { Loading } from "../components/common/Loading";

export const HomePage = () => {
  const [userImages, setUserImages] = useState<UserImage[]>([]);
  const [searchPhrase, setSearchPhrase] = useState(
    localStorage.getItem("search")
  );
  const [page, setPage] = useState(1);
  const [pageTotal, setPageTotal] = useState(12);
  const [isLoading, setIsLoading] = useState(true);

  const handleGetUserImages = (currentPage: number) => {
    const request: QueryRequest = {
      filter: {
        field: "title",
        value: searchPhrase,
      },
      page: {
        startingIndex: currentPage,
        pageTotal: 12,
      },
    };

    getAllUserImages(request)
      .then((response) => {
        setUserImages(response.data.payload);
        setPage(response.data.startingIndex);
        setPageTotal(response.data.pages);
        setIsLoading(false);
      })
      .catch((error) => console.log(error));
  };

  useEffect(() => {
    handleGetUserImages(page);
  }, []);

  if (isLoading) {
    return <Loading />;
  } else {
    return (
      <>
        <UserImageList userImages={userImages} />
        <PagingList
          currentPage={page}
          pageTotal={pageTotal}
          handleGetItems={handleGetUserImages}
        />
      </>
    );
  }
};
