import InterviewDisplay from "../Components/InterviewDisplay/InterviewDisplay";
import useGetLatestHrInterviews from "../../../Hooks/Interviews/HR/useGetLatestHrInterviews";

const GetHrInterviews = () => {
  const { data: hrInterviews, isLoading, isError } = useGetLatestHrInterviews();

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>Error loading HR interviews</div>;

  return (
    <div className="rounded-lg shadow-lg p-3 md:p-6 border border-border bg-background">
      <InterviewDisplay
        interviewType="HR-Interview"
        interviews={hrInterviews}
      />
    </div>
  );
};

export default GetHrInterviews;
