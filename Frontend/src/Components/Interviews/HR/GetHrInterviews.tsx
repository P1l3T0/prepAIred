import InterviewDisplay from "../InterviewDisplay/InterviewDisplay";
import InterviewContainer from "../Common/InterviewContainer";
import useGetLatestHrInterviews from "../../../Hooks/Interviews/HR/useGetLatestHrInterviews";

const GetHrInterviews = () => {
  const { data: hrInterviews, isLoading, isError } = useGetLatestHrInterviews();

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>Error loading HR interviews</div>;

  return (
    <InterviewContainer title="Generated HR Interview">
      <InterviewDisplay
        interviewType="HR-Interview"
        interviews={hrInterviews}
      />
    </InterviewContainer>
  );
};

export default GetHrInterviews;
