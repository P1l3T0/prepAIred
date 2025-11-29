/**
 * Custom hook for fetching recent interview sessions from the backend API.
 * Uses react-query for data fetching and caching.
 * @returns {Object} - data, isLoading, isError
 */
import { useQuery } from "react-query";
import axios, { AxiosError, type AxiosResponse } from "axios";
import { getRecentInterviewSessionsEndPoint } from "../../Utils/endpoints";
import type { InterviewSessionActivity } from "../../Utils/interfaces";

const useGetRecentInterviewSessions = () => {
  const getRecentInterviewSessions = async (): Promise<InterviewSessionActivity[]> => {
    return await axios
      .get<InterviewSessionActivity[]>(getRecentInterviewSessionsEndPoint, { withCredentials: true })
      .then((res: AxiosResponse<InterviewSessionActivity[]>) => {
        return res.data;
      })
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        console.error(error?.title);
        return [];
      });
  };

  const interviewSessionActivitiesQuery = useQuery({
    queryKey: ["interviewSessionActivities"],
    queryFn: getRecentInterviewSessions,
  });

  const { data, isLoading, isError } = interviewSessionActivitiesQuery;

  return { data, isLoading, isError };
};

export default useGetRecentInterviewSessions;
