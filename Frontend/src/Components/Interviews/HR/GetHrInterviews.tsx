import InterviewDisplay from "../Components/InterviewDisplay/InterviewDisplay";
import useGetLatestHrInterviews from "../../../Hooks/Interviews/HR/useGetLatestHrInterviews";
import { Card, CardBody } from "@progress/kendo-react-layout";
import LoaderComponent from "../../Common/LoaderComponent";
import ErrorComponent from "../../Common/ErrorComponent";

const GetHrInterviews = () => {
  const { data: hrInterviews, isLoading, isError } = useGetLatestHrInterviews();

  if (isLoading) return <LoaderComponent />;
  if (isError) return <ErrorComponent />;

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