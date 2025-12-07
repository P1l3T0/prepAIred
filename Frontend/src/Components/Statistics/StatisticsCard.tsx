import { Card, CardBody, CardHeader, CardTitle } from "@progress/kendo-react-layout";

interface StatisticsCardProps {
  title: string;
  children: React.ReactNode;
}

const StatisticsCard = ({ title, children }: StatisticsCardProps) => {
  return (
    <>
      <Card className="shadow-md">
        <CardHeader>
          <CardTitle>{title}</CardTitle>
        </CardHeader>
        <CardBody>{children}</CardBody>
      </Card>
    </>
  );
};

export default StatisticsCard;