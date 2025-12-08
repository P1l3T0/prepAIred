import type { GridCustomCellProps } from "@progress/kendo-react-grid";

const useFormatCells = () => {
  const formatDate = (dateString: string) => {
    const date = new Date(dateString);

    return date.toLocaleDateString("en-US", {
      day: "numeric",
      month: "short",
      hour: "2-digit",
      minute: "2-digit",
    });
  };

  const StatusCell = (props: GridCustomCellProps) => {
    const status = props.dataItem.status;

    const getStatusColor = () => {
      switch (status) {
        case "Passed":
          return "#4caf50";
        case "Failed":
          return "#f44336";
      }
    };

    return (
      <td {...props.tdProps} style={{ color: getStatusColor() }}>
        {props.dataItem.status}
      </td>
    );
  };

  const ScoreCell = (props: GridCustomCellProps) => {
    const score = props.dataItem.averageScore;

    const getScoreColor = () => {
      if (score >= 5) return "#4caf50";

      return "#f44336";
    };

    return (
      <td {...props.tdProps} style={{ color: getScoreColor() }}>
        {score > 0 ? score : "N/A"}
      </td>
    );
  };

  const DateCell = (props: GridCustomCellProps) => {
    return (
      <td {...props.tdProps}>
        {formatDate(props.dataItem.dateCreated)}
      </td>
    )
  };

  return { StatusCell, ScoreCell, DateCell };
};

export default useFormatCells;
