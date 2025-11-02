/**
 * Custom hook for preparing evaluation request data before submission.
 * Helper hook that constructs the evaluation payload from user answers
 * and determines the correct API endpoint based on interview type.
 * 
 * @param {UseEvaluateInterviewsProps} props - Configuration object containing:
 *   - interviewType: Type of interview ("HR-Interview" or "Technical-Interview")
 *   - singleChoiceAnswers: Array of single choice question answers
 *   - multipleChoiceAnswers: Array of multiple choice question answers
 *   - openEndedAnswers: Array of open-ended question answers
 * 
 * @returns {Object} - An object containing:
 *   - getEndpoint: Function that returns the appropriate API endpoint
 *   - prepareEvaluationRequests: Function that formats answers into evaluation request DTOs
 */
import { evaluateHrInterviewsEndPoint, evaluateTechnicalInterviewsEndPoint } from "../../../Utils/endpoints";
import type { EvaluateRequestDTO, UseEvaluateInterviewsProps } from "../../../Utils/interfaces";

const usePrepareEvaluationRequests = ({
  interviewType,
  singleChoiceAnswers,
  multipleChoiceAnswers,
  openEndedAnswers,
}: UseEvaluateInterviewsProps) => {
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

  return { getEndpoint, prepareEvaluationRequests };
};

export default usePrepareEvaluationRequests;
