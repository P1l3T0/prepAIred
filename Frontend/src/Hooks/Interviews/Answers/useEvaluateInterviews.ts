/**
 * Custom hook for evaluating interview answers using AI.
 * Handles the submission of user answers to the backend for evaluation.
 * Works with both HR and technical interview types.
 * 
 * @param {UseEvaluateInterviewsProps} props - Configuration object containing:
 *   - interviewType: Type of interview ("HR-Interview" or "Technical-Interview")
 *   - singleChoiceAnswers: Array of single choice question answers
 *   - multipleChoiceAnswers: Array of multiple choice question answers
 *   - openEndedAnswers: Array of open-ended question answers
 * 
 * @returns {Object} - An object containing:
 *   - handleEvaluateInterviews: Function to trigger evaluation submission
 *   - isSubmitting: Boolean indicating if evaluation is in progress
 */
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
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);

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
    const hasSingleChoiceAnswer = singleChoiceAnswers.every(answer => answer.answer !== "") && singleChoiceAnswers.length > 0;
    const hasOpenEndedAnswer = openEndedAnswers.every(answer => answer.answer !== "") && openEndedAnswers.length > 0;
    const hasMultipleChoiceAnswer = multipleChoiceAnswers.every(answer => answer.answers.every(a => a !== "")) && multipleChoiceAnswers.length > 0;

    if ((!hasSingleChoiceAnswer || !hasOpenEndedAnswer) && !hasMultipleChoiceAnswer) {
      return alert("Please answer at least one question before submitting.");
    }

    e.preventDefault();
    await mutateAsync();
  };

  return { handleEvaluateInterviews, isSubmitting };
};

export default useEvaluateInterviews;
