import InterviewDisplay from "../InterviewDisplay";
import useGetLatestHrInterviews from "../../../Hooks/Interviews/HR/useGetLatestHrInterviews";

const GetHrInterviews = () => {
  const { data: hrInterviews, isLoading, isError } = useGetLatestHrInterviews();

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>Error loading HR interviews</div>;

  return (
    <InterviewDisplay
      title="Latest HR Interviews"
      interviewType="HR-Interview"
      interviews={hrInterviews}
    />
  );
};

export default GetHrInterviews;
