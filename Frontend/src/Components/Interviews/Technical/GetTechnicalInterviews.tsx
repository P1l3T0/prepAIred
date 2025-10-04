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
        <>Subject: {(interview as TechnicalInterviewDTO).subject}</>
      )}
      renderMeta={(interview) => (
        <>
          <b>Diffuculty</b>:{" "}
          {(interview as TechnicalInterviewDTO).difficultyLevel}
        </>
      )}
    />
  );
};

export default GetTechnicalInterviews;
