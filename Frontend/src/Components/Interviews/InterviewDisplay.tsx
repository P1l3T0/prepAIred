import type {
  HRInterviewDTO,
  TechnicalInterviewDTO,
} from "../../Utils/interfaces";
import SingleChoiceAnswers from "./Answers/SingleChoiceAnswers";
import MultipleChoiceAnswers from "./Answers/MultipleChoiceAnswers";
import OpenEndedAnswer from "./Answers/OpenEndedAnswer";
import useEvaluateInterviews from "../../Hooks/Interviews/Answers/useEvaluateInterviews";
import useHandleAnswers from "../../Hooks/Interviews/Answers/useHandleAnswers";
import { Button } from "@progress/kendo-react-buttons";
import "./Interviews.css";

interface InterviewDisplayProps {
  title: string;
  interviewType: "HR-Interview" | "Technical-Interview";
  interviews: HRInterviewDTO[] | TechnicalInterviewDTO[] | void;
}

const InterviewDisplay = ({
  title,
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
    <>
      <h2>{title}</h2>
      <div className="root">
        {interviews && interviews.length > 0 ? (
          interviews.map((interview, interviewIndex) => (
            <fieldset key={interviewIndex} className="fieldset">
              <legend className="legend">
                {interview.interviewType === "HR" ? (
                  <>
                    <b>Competency Area:</b>{" "}
                    {(interview as HRInterviewDTO).competencyArea}
                  </>
                ) : (
                  <>Subject: {(interview as TechnicalInterviewDTO).subject}</>
                )}
                &nbsp; | &nbsp;
                <b>Score:</b>{" "}
                {interview.score || "Not evaluated"}
              </legend>
              <div className="meta">
                {interview.interviewType === "HR" ? (
                  <>
                    <b>Behavioral Context:</b>{" "}
                    {(interview as HRInterviewDTO).behavioralContext}
                  </>
                ) : (
                  <>
                    <b>Position:</b>{" "}
                    {(interview as TechnicalInterviewDTO).position}
                  </>
                )}
              </div>
              <div className="interview">
                <div className="question">{interview.question}</div>
                {(() => {
                  switch (interview.questionType) {
                    case "SingleChoice":
                      const singleChoiceIdx = singleChoiceAnswers.findIndex((a) => a.question === interview.question);
                      return (
                        <SingleChoiceAnswers
                          interview={interview}
                          interviewType={interviewType}
                          interviewIndex={singleChoiceIdx}
                          onChange={(value) =>
                            handleSingleChoiceChange(singleChoiceIdx, value)
                          }
                        />
                      );
                    case "MultipleChoice":
                      const multipleChoiceIdx = multipleChoiceAnswers.findIndex((a) => a.question === interview.question);
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
                          onChange={(value) =>
                            handleOpenEndedChange(openEndedIdx, value)
                          }
                        />
                      );
                    default:
                      return null;
                  }
                })()}
                <div className="feedback">
                  {interview.feedback !== "" ? (
                    <><b>Feedback:</b> {interview.feedback}</>
                  ) : (
                    ""
                  )}
                </div>
              </div>
            </fieldset>
          ))
        ) : (
          <div className="empty">No interviews found</div>
        )}
        <Button
          onClick={handleEvaluateInterviews}
          themeColor={"primary"}
          disabled={
            isSubmitting ||
            !Array.isArray(interviews) ||
            interviews.length === 0 ||
            interviews[0]?.isAnswered
          }
        >
          {isSubmitting ? "Evaluating..." : "Evaluate Interviews"}
        </Button>
      </div>
    </>
  );
};

export default InterviewDisplay;
