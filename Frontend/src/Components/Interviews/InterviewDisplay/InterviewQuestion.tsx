import type {
  HRInterviewDTO,
  TechnicalInterviewDTO,
} from "../../../Utils/interfaces";
import SingleChoiceAnswers from "../Answers/SingleChoiceAnswers";
import MultipleChoiceAnswers from "../Answers/MultipleChoiceAnswers";
import OpenEndedAnswer from "../Answers/OpenEndedAnswer";

interface InterviewQuestionProps {
  interview: HRInterviewDTO | TechnicalInterviewDTO;
  interviewType: "HR-Interview" | "Technical-Interview";
  singleChoiceAnswers: any[];
  multipleChoiceAnswers: any[];
  openEndedAnswers: any[];
  handleSingleChoiceChange: (index: number, value: string) => void;
  handleMultipleChoiceChange: (index: number, value: string) => void;
  handleOpenEndedChange: (index: number, value: string) => void;
}

const InterviewQuestion = ({
  interview,
  interviewType,
  singleChoiceAnswers,
  multipleChoiceAnswers,
  openEndedAnswers,
  handleSingleChoiceChange,
  handleMultipleChoiceChange,
  handleOpenEndedChange,
}: InterviewQuestionProps) => {
  const renderAnswers = () => {
    switch (interview.questionType) {
      case "SingleChoice":
        const singleChoiceIdx = singleChoiceAnswers.findIndex(
          (a) => a.question === interview.question
        );
        return (
          <SingleChoiceAnswers
            interview={interview}
            interviewType={interviewType}
            interviewIndex={singleChoiceIdx}
            onChange={(value) => handleSingleChoiceChange(singleChoiceIdx, value)}
          />
        );
      case "MultipleChoice":
        const multipleChoiceIdx = multipleChoiceAnswers.findIndex(
          (a) => a.question === interview.question
        );
        return (
          <MultipleChoiceAnswers
            interview={interview}
            interviewType={interviewType}
            interviewIndex={multipleChoiceIdx}
            onChange={(value) =>
              handleMultipleChoiceChange(multipleChoiceIdx, value)
            }
          />
        );
      case "OpenEnded":
        const openEndedIdx = openEndedAnswers.findIndex(
          (a) => a.question === interview.question
        );
        return (
          <OpenEndedAnswer
            interview={interview}
            interviewType={interviewType}
            interviewIndex={openEndedIdx}
            onChange={(value) => handleOpenEndedChange(openEndedIdx, value)}
          />
        );
      default:
        return null;
    }
  };

  return (
    <div className="mb-4 pt-3.5 border-t border-border">
      <div className="font-semibold mb-1.5 text-[1.05rem] text-text-primary">
        {interview.question}
      </div>

      <div className="my-3">{renderAnswers()}</div>

      {interview.feedback && (
        <div className="mt-4 p-3 bg-info/10 rounded-lg border border-info/30">
          <b className="text-info">Feedback:</b>
          <span className="text-text-secondary ml-1">{interview.feedback}</span>
        </div>
      )}
    </div>
  );
};

export default InterviewQuestion;
