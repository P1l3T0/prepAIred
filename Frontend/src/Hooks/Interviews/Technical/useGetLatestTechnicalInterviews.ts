import { useQuery } from "react-query";
import axios, { AxiosError, type AxiosResponse } from "axios";
import { getLatestTechnicalInterviewsEndPoint } from "../../../Utils/endpoints";
import type { TechnicalInterviewDTO } from "../../../Utils/interfaces";

const useGetLatestTechnicalInterviews = () => {
  const getLatestTechnicalInterviews = async () => {
    return await axios
      .get<TechnicalInterviewDTO[]>(`${getLatestTechnicalInterviewsEndPoint}`, { withCredentials: true })
      .then((res: AxiosResponse<TechnicalInterviewDTO[]>) => {
        return res.data.map((interview) => ({
          ...interview,
          dateCreated: new Date(interview.dateCreated)
        }));
      })
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        alert(error?.title);
      });
  };

  const technicalInterviewsQuery = useQuery({
    queryKey: ["technical-interviews"],
    queryFn: getLatestTechnicalInterviews
  });

  const { data, isLoading, isError } = technicalInterviewsQuery;

  return { data, isLoading, isError };
};

export default useGetLatestTechnicalInterviews;