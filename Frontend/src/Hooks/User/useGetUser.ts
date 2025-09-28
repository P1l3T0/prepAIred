/**
 * Custom hook for fetching the current user from the backend API.
 * Uses react-query for data fetching and caching.
 * @returns {Object} - data, isLoading, isError
 */
import { useQuery } from "react-query";
import axios, { AxiosError, type AxiosResponse } from "axios";
import { getCurrentUserEndPoint } from "../../Utils/endpoints";
import type { User } from "../../Utils/interfaces";

const useGetUser = () => {
  const getUser = async () => {
    return await axios
      .get<User>(`${getCurrentUserEndPoint}`, { withCredentials: true })
      .then((res: AxiosResponse<User>) => {
        return {
          ...res.data,
          dateCreated: new Date(res.data.dateCreated),
        };
      })
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        console.error(error?.title);
      });
  };

  const userQuery = useQuery({
    queryKey: ["user"],
    queryFn: getUser,
    refetchOnWindowFocus: false,
  });

  const { data, isLoading, isError } = userQuery;

  return { data, isLoading, isError };
};

export default useGetUser;
