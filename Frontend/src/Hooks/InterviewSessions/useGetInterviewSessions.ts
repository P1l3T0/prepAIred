/**
 * Custom hook for fetching the current user from the backend API.
 * Uses react-query for data fetching and caching.
 * @returns {Object} - data, isLoading, isError
 */
import { useQuery } from "react-query";
import axios, { AxiosError, type AxiosResponse } from "axios";
import { getInterviewSessionsEndPoint } from "../../Utils/endpoints";
import type { InterviewSessionDTO } from "../../Utils/interviewTypes";

const useGetInterviewSessions = () => {
  const getInterviewSessions = async () => {
    return await axios
      .get<InterviewSessionDTO[]>(`${getInterviewSessionsEndPoint}`, { withCredentials: true })
      .then((res: AxiosResponse<InterviewSessionDTO[]>) => {
        return res.data.map((interviewSession) => ({
          ...interviewSession,
          interviews: interviewSession.interviews.map((interview) => {
            return {
              ...interview,
              dateCreated: new Date(interview.dateCreated),
            };
          }),
          dateCreated: new Date(interviewSession.dateCreated),
        }));
      })
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        console.error(error?.title);
      });
  };

  const interviewSessionsQuery = useQuery({
    queryKey: ["interview-sessions"],
    queryFn: getInterviewSessions,
    refetchOnWindowFocus: false,
  });

  const { data, isLoading, isError } = interviewSessionsQuery;

  return { data, isLoading, isError };
};

export default useGetInterviewSessions;
