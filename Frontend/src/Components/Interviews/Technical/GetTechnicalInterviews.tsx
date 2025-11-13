import InterviewDisplay from "../Components/InterviewDisplay/InterviewDisplay";
import useGetLatestTechnicalInterviews from "../../../Hooks/Interviews/Technical/useGetLatestTechnicalInterviews";
import type { ChangeStepButtonProps } from "../../../Utils/interfaces";

const GetTechnicalInterviews = ({ handleChangeStep }: ChangeStepButtonProps) => {
  const { data: technicalInterviews, isLoading, isError } = useGetLatestTechnicalInterviews();

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div> Error loading Technical interviews.</div>;

  return (
    <div className="rounded-lg shadow-lg p-3 md:p-6 border border-border bg-background">
      <InterviewDisplay
        interviewType="Technical-Interview"
        interviews={technicalInterviews}
        handleChangeStep={handleChangeStep}
      />
    </div>
  );
};

export default GetTechnicalInterviews;
