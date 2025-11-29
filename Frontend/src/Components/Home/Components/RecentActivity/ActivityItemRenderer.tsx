import type { InterviewSessionActivity } from "../../../../Utils/interfaces";

interface ActivityItemRenderProps {
  dataItem: InterviewSessionActivity;
}

const ActivityItemRender = (props: ActivityItemRenderProps) => {
  const activity = props.dataItem;

  return (
    <div className="k-listview-item my-3">
      <ActivityItem dataItem={activity} />
    </div>
  );
};

const ActivityItem = ({ dataItem }: ActivityItemRenderProps) => {
  const getScoreColor = (score: number) => {
    if (score >= 8) return "text-success";
    if (score >= 6) return "text-primary";
    if (score >= 4) return "text-warning";

    return "text-error";
  };

  const getStatusColor = (status: string) => {
    switch (status) {
      case "Passed":
        return "text-success";
      case "Failed":
        return "text-error";
    }
  };

  return (
    <div className="flex items-center justify-between p-3 bg-text-inverse rounded-lg border border-border-subtle hover:bg-hover transition-colors">
      <div className="flex items-center justify-center space-x-4">
        <div className="w-3 h-3 rounded-full bg-primary" />
        <div className="flex flex-col">
          <h4 className="text-text-primary font-medium">{dataItem.subject}</h4>
          <p className="text-text-secondary text-sm">
            {dataItem.interviewTypes.join(", ")}
          </p>
          <p style={{ margin: 0.5 }} className="text-text-tertiary text-xs">
            {dataItem.programmingLanguage || "N/A"} • {dataItem.aiAgent} •{" "}
            <span className={getStatusColor(dataItem.status)}>
              {dataItem.status}
            </span>
          </p>
        </div>
      </div>
      <div className="text-right">
        <div className={`text-lg font-semibold ${getScoreColor(dataItem.averageScore)}`}>
          {dataItem.averageScore.toFixed(2)}/10
        </div>
        <div className="text-text-tertiary text-sm">
          {new Date(dataItem.dateCreated).toLocaleDateString("en-US", {
            month: "short",
            day: "numeric",
            year: "numeric",
          })}
        </div>
      </div>
    </div>
  );
};

export default ActivityItemRender;
