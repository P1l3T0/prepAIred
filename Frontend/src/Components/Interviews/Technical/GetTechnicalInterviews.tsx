import useGetLatestTechnicalInterviews from "../../../Hooks/Interviews/Technical/useGetLatestTechnicalInterviews";
import SingleChoiceAnswers from "../Answers/SingleChoiceAnswers";
import MultipleChoiceAnswers from "../Answers/MultipleChoiceAnswers";
import OpenEndedAnswer from "../Answers/OpenEndedAnswer";
import useHandleAnswers from "../../../Hooks/Interviews/useHandleAnswers";
import "../Interviews.css";

const GetTechnicalInterviews = () => {
  const { data: technicalInterviews, isLoading, isError } = useGetLatestTechnicalInterviews();
  const {
    handleSingleChoiceChange,
    handleMultipleChoiceChange,
    handleOpenEndedChange,
    singleChoiceAnswers,
    multipleChoiceAnswers,
    openEndedAnswers,
  } = useHandleAnswers(technicalInterviews ?? []);

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>Error loading Technical interviews.</div>;

  return (
    <>
      <h2>Latest Technical Interviews</h2>
      <div className="root">
        <button onClick={() => console.table(singleChoiceAnswers)}>Show Single Choice Answers</button>
        <button onClick={() => console.table(multipleChoiceAnswers)}>Show Multiple Choice Answers</button>
        <button onClick={() => console.table(openEndedAnswers)}>Show Open Ended Answers</button>
        {technicalInterviews && technicalInterviews.length > 0 ? (
          technicalInterviews.map((technicalInterview, interviewIndex) => (
            <fieldset key={interviewIndex} className="fieldset">
              <legend className="legend">
                Subject: {technicalInterview.subject}
              </legend>
              <div className="interview">
                <div className="question">{technicalInterview.question}</div>
                {(() => {
                  switch (technicalInterview.questionType) {
                    case "SingleChoice":
                      const singleChoiceIdx = singleChoiceAnswers.findIndex((a) => a.question === technicalInterview.question);
                      return (
                        <SingleChoiceAnswers
                          interviewType="Technical-Interview"
                          answers={technicalInterview.answers}
                          interviewIndex={singleChoiceIdx}
                          isAnswered={technicalInterview.isAnswered}
                          onChange={(value) =>
                            handleSingleChoiceChange(singleChoiceIdx, value)
                          }
                        />
                      );
                    case "MultipleChoice":
                      const multipleChoiceIdx = multipleChoiceAnswers.findIndex((a) => a.question === technicalInterview.question);
                      return (
                        <MultipleChoiceAnswers
                          interviewType="Technical-Interview"
                          answers={technicalInterview.answers}
                          interviewIndex={multipleChoiceIdx}
                          isAnswered={technicalInterview.isAnswered}
                          onChange={(value) =>
                            handleMultipleChoiceChange(multipleChoiceIdx, value)
                          }
                        />
                      );
                    case "OpenEnded":
                      const openEndedIdx = openEndedAnswers.findIndex((a) => a.question === technicalInterview.question);
                      return (
                        <OpenEndedAnswer
                          interviewType="Technical-Interview"
                          interviewIndex={openEndedIdx}
                          isAnswered={technicalInterview.isAnswered}
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

export default GetTechnicalInterviews;
