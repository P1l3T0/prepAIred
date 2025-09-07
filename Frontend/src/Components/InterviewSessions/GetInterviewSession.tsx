import useGetAiInterviews from "../../Hooks/Interviews/useGetAiInterviews";

const GetAiInterviews = () => {
  const { data: interviewSessions, isLoading, isError } = useGetAiInterviews();

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>Error fetching AI interviews</div>;

  return (
    <>
      {interviewSessions && interviewSessions.length > 0 ? (
        interviewSessions.map((interviewSession) => (
          <fieldset key={interviewSession.id}>
            <legend>Interview Topic: {interviewSession.topic}</legend>
            Generated at: {interviewSession.dateCreated.toLocaleDateString()}
            AI agent used: {interviewSession.aiAgent}
            Score: {interviewSession.score}
            {interviewSession.interviews.map((interview) => (
              <div key={interview.id}>
                <strong>{interview.question}</strong>
                {interview.answers.map((answer, index) => (
                  <span key={index}>
                    <input type="radio" id={`interview-questions-${interview.id}-${index}`} name={`interview-${interview.id}`} value={answer} />
                    <label htmlFor={`interview-questions-${interview.id}-${index}`} style={{ marginLeft: 4 }}>{answer}</label>
                  </span>
                ))}
              </div>
            ))}
          </fieldset>
        ))
      ) : (
        <div>No interview Sessions found</div>
      )}
    </>
  );
}

export default GetAiInterviews