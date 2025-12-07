import { useQuery } from "react-query";
import axios, { AxiosError, type AxiosResponse } from "axios";
import { getInterviewSessionsPositionDataEndPoint } from "../../../Utils/endpoints";
import type { PositionData } from "../../../Utils/interfaces";

const useGetPositionData = () => {
  const getPositionData = async (): Promise<PositionData[]> => {
    return axios
      .get<PositionData[]>(getInterviewSessionsPositionDataEndPoint, { withCredentials: true })
      .then((res: AxiosResponse<PositionData[]>) => res.data)
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        console.error(error?.title);
        return [];
      });
  };

  const positionDataQuery = useQuery({
    queryKey: ["position-data"],
    queryFn: getPositionData,
  });

  const { data, isLoading, isError } = positionDataQuery;

  return { data, isLoading, isError };
};

export default useGetPositionData;
