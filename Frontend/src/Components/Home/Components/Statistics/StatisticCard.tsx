import { Card, CardBody } from "@progress/kendo-react-layout";
import { Label } from "@progress/kendo-react-labels";
import { ProgressBar } from "@progress/kendo-react-progressbars";
import useGetProperColor from "../../../../Hooks/Home/RecentActivity/useGetProperColor";

interface StatisticCardProps {
  title: string;
  value: number | string;
  themeColor: "primary" | "secondary" | "success" | "warning";
  progressValue?: string;
  subtitle: string;
}

const StatisticCard = ({
  title,
  value,
  themeColor,
  progressValue,
  subtitle,
}: StatisticCardProps) => {
  const { getTextColor, getProgressColor } = useGetProperColor(themeColor);

  return (
    <Card className={`border border-border shadow-md`}>
      <CardBody>
        <div className="text-center p-4">
          <div className={`text-3xl font-bold mb-2 ${getTextColor()}`}>
            {value}
          </div>
          <Label className="text-text-secondary font-medium">{title}</Label>
          <div className={`${progressValue ? "mt-2" : "mt-8"}`}>
            {progressValue && (
              <ProgressBar
                progressStyle={{ backgroundColor: getProgressColor() }}
                value={parseFloat(progressValue!) || 0}
                label={(props) => {
                  return <>{Math.round(props.value as number)}%</>;
                }}
              />
            )}
            {subtitle && (
              <Label className={`font-medium ${getTextColor()}`}>
                {subtitle}
              </Label>
            )}
          </div>
        </div>
      </CardBody>
    </Card>
  );
};

export default StatisticCard;
