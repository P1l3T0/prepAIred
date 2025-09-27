import { useQuery } from "react-query";
import axios, { AxiosError, type AxiosResponse } from "axios";
import { getLatestHrInterviewsEndPoint } from "../../../Utils/endpoints";
import type { HRInterviewDTO } from "../../../Utils/interfaces";

const useGetLatestHrInterviews = () => {
  const getLatestHrInterviews = async () => {
    return await axios
      .get<HRInterviewDTO[]>(`${getLatestHrInterviewsEndPoint}`, { withCredentials: true })
      .then((res: AxiosResponse<HRInterviewDTO[]>) => {
        return res.data.map((interview) => ({
          ...interview,
          dateCreated: new Date(interview.dateCreated),
        }));
      })
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        alert(error?.title);
      });
  };

  const hrInterviewsQuery = useQuery({
    queryKey: ["hr-interviews"],
    queryFn: getLatestHrInterviews,
    refetchOnWindowFocus: false,
  });

  const { data, isLoading, isError } = hrInterviewsQuery;

  return { data, isLoading, isError };
};

export default useGetLatestHrInterviews;