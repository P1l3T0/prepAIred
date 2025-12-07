import { useQuery } from "react-query";
import axios, { AxiosError, type AxiosResponse } from "axios";
import { getInterviewSessionsProgrammingLanguageDataEndPoint } from "../../../Utils/endpoints";
import type { ProgrammingLanguageData } from "../../../Utils/interfaces";

const useGetProgrammingLanguageData = () => {
  const getProgrammingLanguageData = async (): Promise<ProgrammingLanguageData[]> => {
    return await axios
      .get<ProgrammingLanguageData[]>(getInterviewSessionsProgrammingLanguageDataEndPoint, { withCredentials: true })
      .then((res: AxiosResponse<ProgrammingLanguageData[]>) => res.data)
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        console.error(error?.title);
        return [];
      });
  };

  const programmingLanguageDataQuery = useQuery({
    queryKey: ["programming-language-data"],
    queryFn: getProgrammingLanguageData,
  });

  const { data, isLoading, isError } = programmingLanguageDataQuery;

  return { data, isLoading, isError };
};

export default useGetProgrammingLanguageData;