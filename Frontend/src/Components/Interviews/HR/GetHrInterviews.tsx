import InterviewDisplay from "../InterviewDisplay";
import useGetLatestHrInterviews from "../../../Hooks/Interviews/HR/useGetLatestHrInterviews";
import type { HRInterviewDTO } from "../../../Utils/interfaces";

const GetHrInterviews = () => {
  const { data: hrInterviews, isLoading, isError } = useGetLatestHrInterviews();

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>Error loading HR interviews</div>;

  return (
    <InterviewDisplay
      title="Latest HR Interviews"
      interviewType="HR-Interview"
      interviews={hrInterviews}
      renderLegend={(interview) => (
        <>Competency Area: {(interview as HRInterviewDTO).competencyArea}</>
      )}
      renderMeta={(interview) => (
        <>
          <b>Behavioral Context:</b>{" "}
          {(interview as HRInterviewDTO).behavioralContext}{" "}
        </>
      )}
    />
  );
};

export default GetHrInterviews;
