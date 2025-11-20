/**
 * Custom hook for fetching the profile picture URL from the backend API.
 * Uses react-query for data fetching and caching.
 * @returns {Object} - data, isLoading, isError
 */
import { useQuery } from "react-query";
import axios, { AxiosError, type AxiosResponse } from "axios";
import { getProfilePictureUrlEndPoint } from "../../Utils/endpoints";

const useGetProfilePictureUrl = () => {
  const getProfilePictureUrl = async () => {
    return await axios
      .get<string>(getProfilePictureUrlEndPoint, { withCredentials: true })
      .then((res: AxiosResponse<string>) => {
        return res.data;
      })
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        console.error(error?.title);
      });
  };

  const profilePictureQuery = useQuery({
    queryKey: ["profile-picture"],
    queryFn: getProfilePictureUrl,
  });

  const { data, isLoading, isError } = profilePictureQuery;

  return { data, isLoading, isError };
};

export default useGetProfilePictureUrl;