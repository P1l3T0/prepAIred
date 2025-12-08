import { useQuery } from "react-query";
import axios, { AxiosError, type AxiosResponse } from "axios";
import { getInterviewSessionsPerformanceEndPoint } from "../../../Utils/endpoints";
import type { PerformanceData } from "../../../Utils/interfaces";

const useGetPerformanceData = () => {
  const getPerformanceData = async (): Promise<PerformanceData[]> => {
    return await axios
      .get<PerformanceData[]>(getInterviewSessionsPerformanceEndPoint, { withCredentials: true })
      .then((res: AxiosResponse<PerformanceData[]>) => {
        return res.data;
      })
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        console.error(error?.title);
        return [];
      });
  };

  const performanceQuery = useQuery({
    queryKey: ["performance-data"],
    queryFn: getPerformanceData,
  });

  const { data, isLoading, isError } = performanceQuery;

  return { data, isLoading, isError };
};

export default useGetPerformanceData;
