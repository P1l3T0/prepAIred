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
