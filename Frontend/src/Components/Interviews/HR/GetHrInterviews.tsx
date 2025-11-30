import InterviewDisplay from "../Components/InterviewDisplay/InterviewDisplay";
import useGetLatestHrInterviews from "../../../Hooks/Interviews/HR/useGetLatestHrInterviews";
import { Loader } from "@progress/kendo-react-indicators";
import { Card, CardBody } from "@progress/kendo-react-layout";

const GetHrInterviews = () => {
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
    <Card className="shadow-lg border border-border">
      <CardBody>
        <InterviewDisplay
          interviewType="HR-Interview"
          interviews={hrInterviews}
        />
      </CardBody>
    </Card>
  );
};

export default GetHrInterviews;
