import InterviewDisplay from "../Components/InterviewDisplay/InterviewDisplay";
import useGetLatestTechnicalInterviews from "../../../Hooks/Interviews/Technical/useGetLatestTechnicalInterviews";
import { Card, CardBody } from "@progress/kendo-react-layout";
import LoaderComponent from "../../Common/LoaderComponent";
import ErrorComponent from "../../Common/ErrorComponent";

const GetTechnicalInterviews = () => {
  const { data: technicalInterviews, isLoading, isError } = useGetLatestTechnicalInterviews();

  if (isLoading) return <LoaderComponent />;
  if (isError) return <ErrorComponent />;

  return (
    <Card className="shadow-lg border border-border">
      <CardBody>
        <InterviewDisplay
          interviewType="Technical-Interview"
          interviews={technicalInterviews}
        />
      </CardBody>
    </Card>
  );
};

export default GetTechnicalInterviews;