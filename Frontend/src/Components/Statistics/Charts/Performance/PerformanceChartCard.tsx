import { Card, CardBody, CardHeader, CardTitle } from "@progress/kendo-react-layout";
import PerformanceChart from "./PerformanceChart";

const PerformanceChartCard = () => {
  return (
    <>
      <Card className="shadow-md">
        <CardHeader>
          <CardTitle>Performance</CardTitle>
        </CardHeader>
        <CardBody>
          <PerformanceChart />
        </CardBody>
      </Card>
    </>
  );
};

export default PerformanceChartCard;