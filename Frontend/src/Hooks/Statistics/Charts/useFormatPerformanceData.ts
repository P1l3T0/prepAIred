import type { TooltipContext } from "@progress/kendo-react-charts";
import type { PerformanceData } from "../../../Utils/interfaces";

const useFormatPerformanceData = (performanceData: PerformanceData[]) => {
  const scores: number[] = performanceData?.map((data) => data.score) || [];

  const categories: string[] =
    performanceData?.map((data) =>
      new Date(data.dateCreated).toLocaleDateString("en-US", {
        day: "2-digit",
        month: "2-digit",
      })
    ) || [];

  const tooltipRender = (props: TooltipContext) =>  `Average score: ${props.point.value}`;

  return { scores, categories, tooltipRender };
};

export default useFormatPerformanceData;