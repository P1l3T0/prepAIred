import InterviewDisplay from "../InterviewDisplay";
import useGetLatestTechnicalInterviews from "../../../Hooks/Interviews/Technical/useGetLatestTechnicalInterviews";
import type { TechnicalInterviewDTO } from "../../../Utils/interfaces";

const GetTechnicalInterviews = () => {
  const { data: technicalInterviews, isLoading, isError } = useGetLatestTechnicalInterviews();

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div> Error loading Technical interviews.</div>;

  return (
    <InterviewDisplay
      title="Latest Technical Interviews"
      interviewType="Technical-Interview"
      interviews={technicalInterviews}
      renderLegend={(interview) => (
        <>Positon: {(interview as TechnicalInterviewDTO).position}</>
      )}
      renderMeta={(interview) => (
        <>
          <b>Subject:</b>{" "}
          {(interview as TechnicalInterviewDTO).subject}
        </>
      )}
    />
  );
};

export default GetTechnicalInterviews;
