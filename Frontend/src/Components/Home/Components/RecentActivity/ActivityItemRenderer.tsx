import type { Activity } from "../../../../Utils/interfaces";

interface ActivityItemRenderProps {
  dataItem: Activity;
}

const ActivityItemRender = (props: ActivityItemRenderProps) => {
  const activity = props.dataItem;

  return (
    <div className="k-listview-item my-4 ">
      <ActivityItem dataItem={activity} />
    </div>
  );
};

const ActivityItem = ({ dataItem }: ActivityItemRenderProps) => {
  const getScoreColor = (score: number) => {
    if (score >= 80) return "text-success";
    if (score >= 60) return "text-primary";
    if (score >= 40) return "text-warning";

    return "text-error";
  };

  return (
    <div className="flex items-center justify-between p-4 bg-text-inverse rounded-lg border border-border-subtle hover:bg-hover transition-colors">
      <div className="flex items-center justify-center space-x-4">
        <div
          className={`w-3 h-3 rounded-full ${
            dataItem.type === "Technical Interview"
              ? "bg-primary"
              : "bg-secondary"
          }`}
        ></div>
        <div className="flex flex-col">
          <h4 className="text-text-primary font-medium">{dataItem.subject}</h4>
          <p style={{ margin: 0 }} className="text-text-secondary text-sm">
            {dataItem.type}
          </p>
        </div>
      </div>
      <div className="text-right">
        <div
          className={`text-lg font-semibold ${getScoreColor(dataItem.score)}`}
        >
          {dataItem.score}%
        </div>
        <div className="text-text-tertiary text-sm">{dataItem.date}</div>
      </div>
    </div>
  );
};

export default ActivityItemRender;
