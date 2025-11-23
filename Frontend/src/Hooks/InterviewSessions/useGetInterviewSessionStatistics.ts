/**
 * Custom hook for fetching interview session statistics from the backend API.
 * Uses react-query for data fetching and caching.
 * @returns {Object} - data, isLoading, isError
 */
import { useQuery } from "react-query";
import axios, { AxiosError, type AxiosResponse } from "axios";
import { getInterviewSessionStatisticsEndPoint } from "../../Utils/endpoints";
import type { ProfileStats } from "../../Utils/interfaces";

const useGetInterviewSessionStatistics = () => {
  const getInterviewSessionStatistics = async () => {
    return await axios
      .get<ProfileStats>(getInterviewSessionStatisticsEndPoint, { withCredentials: true })
      .then((res: AxiosResponse<ProfileStats>) => {
        return res.data;
      })
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        console.error(error?.title);
      });
  };

  const statisticsQuery = useQuery({
    queryKey: ["interviewSessionStatistics"],
    queryFn: getInterviewSessionStatistics
  });

  const { data, isLoading, isError } = statisticsQuery;

  return { data, isLoading, isError };
};

export default useGetInterviewSessionStatistics;