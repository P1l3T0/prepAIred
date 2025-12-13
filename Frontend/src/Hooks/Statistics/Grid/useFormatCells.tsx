import type { GridCustomCellProps } from "@progress/kendo-react-grid";

const useFormatCells = () => {
  const StatusCell = (props: GridCustomCellProps) => {
    const status: string = props.dataItem.status;

    const getStatusColor = () => {
      if (status === "Passed") return "text-success";
      if (status === "Failed") return "text-error";
    };

    return (
      <td {...props.tdProps}>
        <span className={`${getStatusColor()}`}>{status}</span>
      </td>
    );
  };

  const ScoreCell = (props: GridCustomCellProps) => {
    const score: number = props.dataItem.averageScore;

    const getScoreColor = () => {
      if (score >= 8) return "text-primary";
      if (score >= 5) return "text-success";
      if (score >= 3) return "text-warning";

      return "text-error";
    };

    return (
      <td {...props.tdProps}>
        <span className={`${getScoreColor()}`}>
          {score > 0 ? score : "N/A"}
        </span>
      </td>
    );
  };

  const DateCell = (props: GridCustomCellProps) => {
    const dateCreated: string = props.dataItem.dateCreated;
    const formattedDate: string = new Date(dateCreated).toLocaleDateString("en-US", {
      day: "numeric",
      month: "short",
      hour: "2-digit",
      minute: "2-digit",
    });

    return (
      <td {...props.tdProps}>
        <span>
          {formattedDate}
        </span>
      </td>
    );
  };

  return { StatusCell, ScoreCell, DateCell };
};

export default useFormatCells;
