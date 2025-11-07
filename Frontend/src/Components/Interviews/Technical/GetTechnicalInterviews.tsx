import InterviewDisplay from "../InterviewDisplay/InterviewDisplay";
import useGetLatestTechnicalInterviews from "../../../Hooks/Interviews/Technical/useGetLatestTechnicalInterviews";

const GetTechnicalInterviews = () => {
  const { data: technicalInterviews, isLoading, isError } = useGetLatestTechnicalInterviews();

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div> Error loading Technical interviews.</div>;

  return (
    <div className="rounded-lg shadow-lg p-6 border border-border">
      <InterviewDisplay
        interviewType="Technical-Interview"
        interviews={technicalInterviews}
      />
    </div>
  );
};

export default GetTechnicalInterviews;
