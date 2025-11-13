import type {
  HRInterviewDTO,
  TechnicalInterviewDTO,
} from "../../../../Utils/interfaces";
import useEvaluateInterviews from "../../../../Hooks/Interviews/Answers/useEvaluateInterviews";
import useHandleAnswers from "../../../../Hooks/Interviews/Answers/useHandleAnswers";
import EvaluationButton from "../Common/EvaluationButton";
import InterviewFieldset from "./InterviewFieldset";
import InterviewQuestion from "./InterviewQuestion";

interface InterviewDisplayProps {
  interviewType: "HR-Interview" | "Technical-Interview";
  interviews: HRInterviewDTO[] | TechnicalInterviewDTO[] | void;
}

const InterviewDisplay = ({
  interviews,
  interviewType,
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

  const { handleEvaluateInterviews, isSubmitting } = useEvaluateInterviews({
    interviewType,
    singleChoiceAnswers,
    multipleChoiceAnswers,
    openEndedAnswers,
  });

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
          No interviews found
        </div>
      )}

      <EvaluationButton
        onClick={handleEvaluateInterviews}
        isSubmitting={isSubmitting}
        disabled={
          isSubmitting ||
          !Array.isArray(interviews) ||
          interviews.length === 0 ||
          interviews[0]?.isAnswered
        }
      />
    </div>
  );
};

export default InterviewDisplay;
