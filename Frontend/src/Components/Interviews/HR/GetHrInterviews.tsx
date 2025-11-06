import InterviewDisplay from "../InterviewDisplay";
import useGetLatestHrInterviews from "../../../Hooks/Interviews/HR/useGetLatestHrInterviews";

const GetHrInterviews = () => {
  const { data: hrInterviews, isLoading, isError } = useGetLatestHrInterviews();

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>Error loading HR interviews</div>;

  return (
    <div className="bg-card rounded-lg shadow-lg p-6 border border-border">
      <h3 className="text-xl font-bold text-text-primary mb-2">
        Generated Interviews
      </h3>
      <p className="text-sm text-text-secondary mb-6">
        Your most recent HR interview questions
      </p>
      <InterviewDisplay
        interviewType="HR-Interview"
        interviews={hrInterviews}
      />
    </div>
  );
};

export default GetHrInterviews;
