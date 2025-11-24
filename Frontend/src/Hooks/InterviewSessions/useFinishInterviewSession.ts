import axios, { AxiosError } from "axios";
import { useMutation, useQueryClient } from "react-query";
import { finishInterviewSessionEndPoint } from "../../Utils/endpoints";
import { useInterviewStep } from "../../Context/InterviewStep/useInterviewStep";
import useInterviewGenerateButton from "../../Context/InterviewGenerateButton/useInterviewGenerateButton";

const useFinishInterviewSession = () => {
  const queryClient = useQueryClient();
  const { handleChangeStep } = useInterviewStep();
  const { setDisableHrInterviewButton, setDisableTechnicalInterviewButton } = useInterviewGenerateButton();

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
      handleChangeStep(0);
      setDisableHrInterviewButton(false);
      setDisableTechnicalInterviewButton(false);
      queryClient.invalidateQueries({ queryKey: ["interviewSessionStatistics"] });
      queryClient.invalidateQueries({ queryKey: ["hr-interviews"] });
      queryClient.invalidateQueries({ queryKey: ["technical-interviews"] });
    },
  });

  const handleFinishInterviewSessionClick = async (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    await mutateAsync();
  };

  return { handleFinishInterviewSessionClick };
};

export default useFinishInterviewSession;
