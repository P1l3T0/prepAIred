import {
  Card,
  CardBody,
  CardHeader,
  CardTitle,
} from "@progress/kendo-react-layout";
import StatisticsGrid from "./StatisticsGrid";

const StatisticsGridCard = () => {
  return (
    <>
      <Card className="shadow-md">
        <CardHeader>
          <CardTitle>Interview Sessions</CardTitle>
        </CardHeader>
        <CardBody>
          <StatisticsGrid />
        </CardBody>
      </Card>
    </>
  );
};

export default StatisticsGridCard;
