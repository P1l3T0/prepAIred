import SingleChoiceAnswers from "./Answers/SingleChoiceAnswers";
import MultipleChoiceAnswers from "./Answers/MultipleChoiceAnswers";
import OpenEndedAnswer from "./Answers/OpenEndedAnswer";
import useHandleAnswers from "../../Hooks/Interviews/useHandleAnswers";
import type { HRInterviewDTO, InterviewDisplayProps, TechnicalInterviewDTO } from "../../Utils/interfaces";
import "./Interviews.css";

const InterviewDisplay = ({
  title,
  interviews,
  interviewType,
  renderLegend,
  renderMeta
}: InterviewDisplayProps) => {
  const {
    handleSingleChoiceChange,
    handleMultipleChoiceChange,
    handleOpenEndedChange,
    singleChoiceAnswers,
    multipleChoiceAnswers,
    openEndedAnswers
  } = useHandleAnswers((interviews as HRInterviewDTO[] | TechnicalInterviewDTO[]) ?? []);

  return (
    <>
      <h2>{title}</h2>
      <div className="root">
        {interviews && interviews.length > 0 ? (
          interviews.map((interview, interviewIndex) => (
            <fieldset key={interviewIndex} className="fieldset">
              <legend className="legend">
                {renderLegend(interview)}
              </legend>
              {renderMeta && (
                <div className="meta">
                  {renderMeta(interview)}
                </div>
              )}
              <div className="interview">
                <div className="question">{interview.question}</div>
                {(() => {
                  switch (interview.questionType) {
                    case "SingleChoice":
                      const singleChoiceIdx = singleChoiceAnswers.findIndex((a) => a.question === interview.question);
                      return (
                        <SingleChoiceAnswers
                          interviewType={interviewType}
                          answers={interview.answers}
                          interviewIndex={singleChoiceIdx}
                          isAnswered={interview.isAnswered}
                          onChange={(value) =>
                            handleSingleChoiceChange(singleChoiceIdx, value)
                          }
                        />
                      );
                    case "MultipleChoice":
                      const multipleChoiceIdx = multipleChoiceAnswers.findIndex((a) => a.question === interview.question);
                      return (
                        <MultipleChoiceAnswers
                          interviewType={interviewType}
                          answers={interview.answers}
                          interviewIndex={multipleChoiceIdx}
                          isAnswered={interview.isAnswered}
                          onChange={(value) =>
                            handleMultipleChoiceChange(multipleChoiceIdx, value)
                          }
                        />
                      );
                    case "OpenEnded":
                      const openEndedIdx = openEndedAnswers.findIndex((a) => a.question === interview.question);
                      return (
                        <OpenEndedAnswer
                          interviewType={interviewType}
                          interviewIndex={openEndedIdx}
                          isAnswered={interview.isAnswered}
                          onChange={(value) =>
                            handleOpenEndedChange(openEndedIdx, value)
                          }
                        />
                      );
                    default:
                      return null;
                  }
                })()}
              </div>
            </fieldset>
          ))
        ) : (
          <div className="empty">No interviews found</div>
        )}
      </div>
    </>
  );
};

export default InterviewDisplay;