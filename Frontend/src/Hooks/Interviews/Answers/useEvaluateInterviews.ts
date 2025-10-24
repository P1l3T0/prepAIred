import axios, { AxiosError } from "axios";
import { useState, type SyntheticEvent } from "react";
import { useMutation, useQueryClient } from "react-query";
import type { UseEvaluateInterviewsProps } from "../../../Utils/interfaces";
import usePrepareEvaluationRequests from "./usePrepareEvaluationRequests";

const useEvaluateInterviews = ({
  interviewType,
  singleChoiceAnswers,
  multipleChoiceAnswers,
  openEndedAnswers,
}: UseEvaluateInterviewsProps) => {
  const queryClient = useQueryClient();
  const [isSubmitting, setIsSubmitting] = useState(false);

  const { getEndpoint, prepareEvaluationRequests } = usePrepareEvaluationRequests({
    interviewType,
    singleChoiceAnswers,
    multipleChoiceAnswers,
    openEndedAnswers,
  });

  const evaluateInterviews = async () => {
    const endpoint = getEndpoint();
    const evaluationRequests = prepareEvaluationRequests();

    await axios
      .post(endpoint, evaluationRequests, { withCredentials: true })
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        alert(error?.title);
      });
  };

  const { mutateAsync } = useMutation({
    mutationFn: evaluateInterviews,
    onMutate: () => {
      setIsSubmitting(true);
    },
    onSuccess: () => {
      const queryKey =
        interviewType === "HR-Interview"
          ? ["hr-interviews"]
          : ["technical-interviews"];

      queryClient.invalidateQueries({ queryKey });
      setIsSubmitting(false);
    }
  });

  const handleEvaluateInterviews = async (e: SyntheticEvent) => {
    e.preventDefault();
    await mutateAsync();
  };

  return { handleEvaluateInterviews, isSubmitting };
};

export default useEvaluateInterviews;
