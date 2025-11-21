import { useState } from "react";
import type { PageChangeEvent } from "@progress/kendo-react-data-tools";
import type { Activity } from "../../Utils/interfaces";

const useChangePage = (recentActivity: Activity[]) => {
  const [page, setPage] = useState({
    skip: 0,
    take: 4,
  });

  const handlePageChange = (e: PageChangeEvent) => {
    setPage({
      skip: e.skip,
      take: e.take,
    });
  };

  const { skip, take } = page;
  const pagedData = recentActivity.slice(skip, skip + take);

  return { skip, take, pagedData, handlePageChange };
}

export default useChangePage;