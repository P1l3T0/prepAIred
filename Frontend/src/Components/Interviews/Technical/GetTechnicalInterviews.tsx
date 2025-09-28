import SingleChoiceAnswers from "../Answers/SingleChoiceAnswers";
import MultipleChoiceAnswers from "../Answers/MultipleChoiceAnswers";
import OpenEndedAnswer from "../Answers/OpenEndedAnswer";
import "../Interviews.css";
import useGetLatestTechnicalInterviews from "../../../Hooks/Interviews/Technical/useGetLatestHrInterviews";

const GetTechnicalInterviews = () => {
  const { data: technicalInterviews, isLoading, isError } = useGetLatestTechnicalInterviews();

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>Error loading HR interviews.</div>;

  return (
    <>
      <h2>Latest Technical Interviews</h2>
      <div className="root">
        {technicalInterviews && technicalInterviews.length > 0 ? (
          technicalInterviews.map((technicalInterview, interviewIndex) => (
            <fieldset key={interviewIndex} className="fieldset">
              <legend className="legend">
                Programming Language: {technicalInterview.programmingLanguage} | Position: {technicalInterview.position}
              </legend>
              <div className="meta">
                <b>Subject:</b> {technicalInterview.subject}
              </div>
              <div className="interview">
                <div className="question">{technicalInterview.question}</div>
                {(() => {
                  switch (technicalInterview.questionType) {
                    case "SingleChoice":
                      return (
                        <SingleChoiceAnswers
                          interviewType="Technical-Interview"
                          answers={technicalInterview.answers}
                          interviewIndex={interviewIndex}
                          isAnswered={technicalInterview.isAnswered}
                        />
                      );
                    case "MultipleChoice":
                      return (
                        <MultipleChoiceAnswers
                          interviewType="Technical-Interview"
                          answers={technicalInterview.answers}
                          interviewIndex={interviewIndex}
                          isAnswered={technicalInterview.isAnswered}
                        />
                      );
                    case "OpenEnded":
                      return (
                        <OpenEndedAnswer
                          interviewType="Technical-Interview"
                          interviewIndex={interviewIndex}
                          isAnswered={technicalInterview.isAnswered}
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
