import { Card, CardBody } from "@progress/kendo-react-layout";
import { Label } from "@progress/kendo-react-labels";
import { ProgressBar } from "@progress/kendo-react-progressbars";

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
  const getTextColor = (): string => {
    switch (themeColor) {
      case "primary":
        return "text-primary";
      case "secondary":
        return "text-secondary";
      case "success":
        return "text-success";
      case "warning":
        return "text-warning";
      default:
        return "text-primary";
    }
  };

  const getProgressColor = (): string => {
    switch (themeColor) {
      case "primary":
        return "#3b82f6";
      case "secondary":
        return "#a78bfa";
      case "success":
        return "#34d399";
      case "warning":
        return "#fbbf24";
      default:
        return "#8b5cf6";
    }
  };

  return (
    <Card className={`border border-border shadow-sm shadow-${themeColor}`}>
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
                  return <>{Math.round((props.value as number))}%</>;
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
