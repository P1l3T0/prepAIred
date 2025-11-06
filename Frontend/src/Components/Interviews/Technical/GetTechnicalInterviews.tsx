import InterviewDisplay from "../InterviewDisplay/InterviewDisplay";
import InterviewContainer from "../Common/InterviewContainer";
import useGetLatestTechnicalInterviews from "../../../Hooks/Interviews/Technical/useGetLatestTechnicalInterviews";

const GetTechnicalInterviews = () => {
  const { data: technicalInterviews, isLoading, isError } = useGetLatestTechnicalInterviews();

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div> Error loading Technical interviews.</div>;

  return (
    <InterviewContainer title="Generated Technical Interview">
      <InterviewDisplay
        interviewType="Technical-Interview"
        interviews={technicalInterviews}
      />
    </InterviewContainer>
  );
};

export default GetTechnicalInterviews;
