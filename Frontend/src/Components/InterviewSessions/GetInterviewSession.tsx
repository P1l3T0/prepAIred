import useGetAiInterviews from "../../Hooks/InterviewSessions/useGetInterviewSessions";
import "./GetInterviewSession.css";

const GetAiInterviews = () => {
  const { data: interviewSessions, isLoading, isError } = useGetAiInterviews();

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>Error fetching AI interviews</div>;

  return (
    <>
      <div className="root">
        {interviewSessions && interviewSessions.length > 0 ? (
          interviewSessions.map((interviewSession, interviewSessionIndex) => (
            <fieldset key={interviewSessionIndex} className="fieldset">
              <legend className="legend">
                Interview Topic: {interviewSession.topic}
              </legend>
              <div className="meta">
                Generated at:{" "}
                {interviewSession.dateCreated.toLocaleDateString()} | AI agent:{" "}
                {interviewSession.aiAgent} | Score: {interviewSession.score}
              </div>
              {interviewSession.interviews.map((interview, interviewIndex) => (
                <div key={interviewIndex} className="interview">
                  <div className="question">{interview.question}</div>
                  <div>
                    {(() => {
                      switch (interview.questionType) {
                        case "SingleChoice":
                          return interview.answers.map((answer, answerIndex) => (
                            <div key={answerIndex} className="answer">
                              <input
                                type="radio"
                                id={`interview-questions-${interviewIndex}-${answerIndex}`}
                                name={`interview-${interviewIndex}`}
                                value={answer}
                                disabled={interviewSession.isCompleted}
                              />
                              <label htmlFor={`interview-questions-${interviewIndex}-${answerIndex}`}>
                                {answer}
                              </label>
                            </div>
                          ));
                        case "MultipleChoice":
                          return interview.answers.map((answer, answerIndex) => (
                            <div key={answerIndex} className="answer">
                              <input
                                type="checkbox"
                                id={`interview-questions-${interviewIndex}-${answerIndex}`}
                                name={`interview-${interviewIndex}`}
                                value={answer}
                                disabled={interviewSession.isCompleted}
                              />
                              <label htmlFor={`interview-questions-${interviewIndex}-${answerIndex}`}>
                                {answer}
                              </label>
                            </div>
                          ));
                        case "OpenEnded":
                          return (
                            <textarea
                              className="answer"
                              name={`interview-${interviewIndex}`}
                              placeholder="Type your answer..."
                              disabled={interviewSession.isCompleted}
                            />
                          );
                        default:
                          return null;
                      }
                    })()}
                  </div>
                </div>
              ))}
            </fieldset>
          ))
        ) : (
          <div className="empty">No interview sessions found</div>
        )}
      </div>
    </>
  );
}

export default GetAiInterviews