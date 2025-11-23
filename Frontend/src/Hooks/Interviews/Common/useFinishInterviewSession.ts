import axios, { AxiosError } from "axios";
import { useMutation, useQueryClient } from "react-query";
import { finishInterviewSessionEndPoint } from "../../../Utils/endpoints";
import { useInterviewStep } from "../../../Context/InterviewStep/useInterviewStep";

const useFinishInterviewSession = () => {
  const queryClient = useQueryClient();
  const { handleChangeStep } = useInterviewStep();

  const finishInterviewSession = async () => {
    await axios
      .put(finishInterviewSessionEndPoint, {}, { withCredentials: true })
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        alert(error?.title);
      });
  };

  const { mutateAsync } = useMutation({
    mutationFn: finishInterviewSession,
    onSuccess: () => {
      handleChangeStep();
      queryClient.invalidateQueries({ queryKey: ["interviewSessionStatistics"] });
      queryClient.invalidateQueries({ queryKey: ["hr-interviews"] });
      queryClient.invalidateQueries({ queryKey: ["technical-interviews"] });
    },
  });

  const handleFinishInterviewSessionClick = async (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    mutateAsync();
  };

  return { handleFinishInterviewSessionClick };
};

export default useFinishInterviewSession;
