import useGetLatestHrInterviews from "../../../Hooks/Interviews/HR/useGetLatestHrInterviews";
import SingleChoiceAnswers from "../Answers/SingleChoiceAnswers";
import MultipleChoiceAnswers from "../Answers/MultipleChoiceAnswers";
import OpenEndedAnswer from "../Answers/OpenEndedAnswer";
import "../Interviews.css";

const GetHrInterviews = () => {
  const { data: hrInterviews, isLoading, isError } = useGetLatestHrInterviews();

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
                      return (
                        <SingleChoiceAnswers
                          interviewType="HR-Interview"
                          answers={hrInterview.answers}
                          interviewIndex={interviewIndex}
                          isAnswered={hrInterview.isAnswered}
                        />
                      );
                    case "MultipleChoice":
                      return (
                        <MultipleChoiceAnswers
                          interviewType="HR-Interview"
                          answers={hrInterview.answers}
                          interviewIndex={interviewIndex}
                          isAnswered={hrInterview.isAnswered}
                        />
                      );
                    case "OpenEnded":
                      return (
                        <OpenEndedAnswer
                          interviewType="HR-Interview"
                          interviewIndex={interviewIndex}
                          isAnswered={hrInterview.isAnswered}
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
