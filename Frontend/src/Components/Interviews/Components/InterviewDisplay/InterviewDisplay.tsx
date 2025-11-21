import type {
  HRInterviewDTO,
  TechnicalInterviewDTO,
} from "../../../../Utils/interfaces";
import useEvaluateInterviews from "../../../../Hooks/Interviews/Answers/useEvaluateInterviews";
import useHandleAnswers from "../../../../Hooks/Interviews/Answers/useHandleAnswers";
import EvaluationButton from "../Common/EvaluationButton";
import InterviewFieldset from "./InterviewFieldset";
import InterviewQuestion from "./InterviewQuestion";
import ChangeStepButton from "../Common/ChangeStepButton";

interface InterviewDisplayProps {
  interviewType: "HR-Interview" | "Technical-Interview";
  interviews: HRInterviewDTO[] | TechnicalInterviewDTO[] | void;
  handleChangeStep: () => void;
}

const InterviewDisplay = ({
  interviews,
  interviewType,
  handleChangeStep,
}: InterviewDisplayProps) => {
  const {
    handleSingleChoiceChange,
    handleMultipleChoiceChange,
    handleOpenEndedChange,
    singleChoiceAnswers,
    multipleChoiceAnswers,
    openEndedAnswers,
  } = useHandleAnswers(
    (interviews as HRInterviewDTO[] | TechnicalInterviewDTO[]) ?? []
  );

  const label =
    interviewType === "HR-Interview"
      ? "Proceed to Technical Interview"
      : "Back to HR Interview";
  
  const { handleEvaluateInterviews, isSubmitting } = useEvaluateInterviews({
    interviewType,
    singleChoiceAnswers,
    multipleChoiceAnswers,
    openEndedAnswers,
  });

  const disableHrInterviews: boolean =
    isSubmitting ||
    !Array.isArray(interviews) ||
    interviews.length === 0 ||
    interviews.every((interview) => interview.isAnswered);

  const disableTechnicalInterviews: boolean =
    !Array.isArray(interviews) ||
    (interviewType === "HR-Interview" &&
      (interviews.length === 0 ||
        interviews.some((interview) => !interview.isAnswered)));

  return (
    <div className="p-2 md:p-4 bg-card rounded-lg shadow-sm">
      {interviews && interviews.length > 0 ? (
        <div className="space-y-8">
          {interviews.map((interview, interviewIndex) => (
            <InterviewFieldset key={interviewIndex} interview={interview}>
              <InterviewQuestion
                interview={interview}
                interviewType={interviewType}
                singleChoiceAnswers={singleChoiceAnswers}
                multipleChoiceAnswers={multipleChoiceAnswers}
                openEndedAnswers={openEndedAnswers}
                handleSingleChoiceChange={handleSingleChoiceChange}
                handleMultipleChoiceChange={handleMultipleChoiceChange}
                handleOpenEndedChange={handleOpenEndedChange}
              />
            </InterviewFieldset>
          ))}
        </div>
      ) : (
        <div className="text-center text-text-tertiary text-lg my-5">
          No interview found
        </div>
      )}

      <div className="justify-between gap-4 flex flex-col md:flex-row">
        <EvaluationButton
          isSubmitting={isSubmitting}
          disabled={disableHrInterviews}
          onClick={handleEvaluateInterviews}
        />

        <ChangeStepButton
          label={label}
          disabled={disableTechnicalInterviews}
          onClick={handleChangeStep}
        />
      </div>
    </div>
  );
};

export default InterviewDisplay;
