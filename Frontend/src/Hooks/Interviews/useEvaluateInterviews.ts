import axios, { AxiosError } from "axios";
import { useState, type SyntheticEvent } from "react";
import { useMutation, useQueryClient } from "react-query";
import { evaluateHrInterviewsEndPoint, evaluateTechnicalInterviewsEndPoint } from "../../Utils/endpoints";
import type { EvaluateRequestDTO, UseEvaluateInterviewsProps } from "../../Utils/interfaces";

const useEvaluateInterviews = ({
  interviewType,
  singleChoiceAnswers,
  multipleChoiceAnswers,
  openEndedAnswers,
}: UseEvaluateInterviewsProps) => {
  const queryClient = useQueryClient();
  const [isSubmitting, setIsSubmitting] = useState(false);

  const getEndpoint = () => {
    return interviewType === "HR-Interview"
      ? evaluateHrInterviewsEndPoint
      : evaluateTechnicalInterviewsEndPoint;
  };

  const prepareEvaluationRequests = (): EvaluateRequestDTO[] => {
    const evaluationRequests: EvaluateRequestDTO[] = [];

    singleChoiceAnswers.forEach((answer) => {
      if (answer.answer.trim()) {
        evaluationRequests.push({
          question: answer.question,
          answer: answer.answer,
        });
      }
    });

    multipleChoiceAnswers.forEach((answer) => {
      if (answer.answers.length > 0) {
        evaluationRequests.push({
          question: answer.question,
          answer: answer.answers,
        });
      }
    });

    openEndedAnswers.forEach((answer) => {
      if (answer.answer.trim()) {
        evaluationRequests.push({
          question: answer.question,
          answer: answer.answer,
        });
      }
    });

    return evaluationRequests;
  };

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
