import InterviewDisplay from "../Components/InterviewDisplay/InterviewDisplay";
import useGetLatestHrInterviews from "../../../Hooks/Interviews/HR/useGetLatestHrInterviews";
import type { ChangeStepButtonProps } from "../../../Utils/interfaces";
import { Loader } from "@progress/kendo-react-indicators";
import { Card, CardBody } from "@progress/kendo-react-layout";

const GetHrInterviews = ({ handleChangeStep }: ChangeStepButtonProps) => {
  const { data: hrInterviews, isLoading, isError } = useGetLatestHrInterviews();

  if (isLoading) {
    return (
    <div className="bg-background flex items-center justify-center">
      <div className="flex flex-col items-center space-y-4">
        <Loader size="large" />
        <span className="text-text-secondary">Loading HR Interview...</span>
      </div>
    </div>
    );
  }

  if (isError) {
    return (
      <div className="bg-background flex items-center justify-center p-4">
        <Card className="shadow-lg">
          <CardBody>
            <div className="text-center p-8">
              <h2 className="text-xl text-text-primary font-semibold mb-2">
                Unable to load HR Interview
              </h2>
              <p className="text-text-secondary">
                Please try refreshing the page
              </p>
            </div>
          </CardBody>
        </Card>
      </div>
    );
  }

  return (
    <div className="rounded-lg shadow-lg p-3 md:p-6 border border-border bg-background">
      <InterviewDisplay
        interviewType="HR-Interview"
        interviews={hrInterviews}
        handleChangeStep={handleChangeStep}
      />
    </div>
  );
};

export default GetHrInterviews;
