import useGetLatestHrInterviews from "../../../Hooks/Interviews/HR/useGetLatestHrInterviews";
import SingleChoiceAnswers from "../Answers/SingleChoiceAnswers";
import MultipleChoiceAnswers from "../Answers/MultipleChoiceAnswers";
import OpenEndedAnswer from "../Answers/OpenEndedAnswer";
import useHandleAnswers from "../../../Hooks/Interviews/useHandleAnswers";
import "../Interviews.css";

const GetHrInterviews = () => {
  const { data: hrInterviews, isLoading, isError } = useGetLatestHrInterviews();
  const {
    handleSingleChoiceChange,
    handleMultipleChoiceChange,
    handleOpenEndedChange,
    singleChoiceAnswers,
    multipleChoiceAnswers,
    openEndedAnswers,
  } = useHandleAnswers(hrInterviews ?? []);

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>Error loading HR interviews.</div>;

  return (
    <>
      <h2>Latest HR Interviews</h2>
      <div className="root">
        {hrInterviews && hrInterviews.length > 0 ? (
          hrInterviews.map((hrInterview, interviewIndex) => (
            <fieldset key={interviewIndex} className="fieldset">
              <legend className="legend">
                Competency Area: {hrInterview.competencyArea}
              </legend>
              <div className="meta">
                <b>Behavioral Context:</b> {hrInterview.behavioralContext}{" "}
              </div>
              <div className="interview">
                <div className="question">{hrInterview.question}</div>
                {(() => {
                  switch (hrInterview.questionType) {
                    case "SingleChoice":
                      const singleChoiceIdx = singleChoiceAnswers.findIndex((a) => a.question === hrInterview.question);
                      return (
                        <SingleChoiceAnswers
                          interviewType="HR-Interview"
                          answers={hrInterview.answers}
                          interviewIndex={singleChoiceIdx}
                          isAnswered={hrInterview.isAnswered}
                          onChange={(value) =>
                            handleSingleChoiceChange(singleChoiceIdx, value)
                          }
                        />
                      );
                    case "MultipleChoice":
                      const multipleChoiceIdx = multipleChoiceAnswers.findIndex((a) => a.question === hrInterview.question);
                      return (
                        <MultipleChoiceAnswers
                          interviewType="HR-Interview"
                          answers={hrInterview.answers}
                          interviewIndex={multipleChoiceIdx}
                          isAnswered={hrInterview.isAnswered}
                          onChange={(value) =>
                            handleMultipleChoiceChange(multipleChoiceIdx, value)
                          }
                        />
                      );
                    case "OpenEnded":
                      const openEndedIdx = openEndedAnswers.findIndex((a) => a.question === hrInterview.question);
                      return (
                        <OpenEndedAnswer
                          interviewType="HR-Interview"
                          interviewIndex={openEndedIdx}
                          isAnswered={hrInterview.isAnswered}
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
          <div className="empty">No interview sessions found</div>
        )}
      </div>
    </>
  );
};

export default GetHrInterviews;
