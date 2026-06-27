import { Chart, ChartLegend, ChartCategoryAxis, ChartCategoryAxisItem, ChartSeries, ChartSeriesItem, ChartTooltip } from "@progress/kendo-react-charts";
import useGetPerformanceData from "../../../Hooks/Statistics/Charts/useGetPerformanceData";
import useFormatPerformanceData from "../../../Hooks/Statistics/Charts/useFormatPerformanceData";
import LoaderComponent from "../../Common/LoaderComponent";
import ErrorComponent from "../../Common/ErrorComponent";

const PerformanceChart = () => {
  const { data: performanceData, isLoading, isError } = useGetPerformanceData();
  const { scores, categories, tooltipRender } = useFormatPerformanceData(performanceData || []);

  if (isLoading) return <LoaderComponent />;
  if (isError) return <ErrorComponent />;

  return (
    <>
      {performanceData && performanceData.length > 0 ? (
        <Chart>
          <ChartLegend visible={false} />
          <ChartCategoryAxis>
            <ChartCategoryAxisItem categories={categories} />
          </ChartCategoryAxis>
          <ChartSeries>
            <ChartSeriesItem type="line" name="Score" style="smooth" data={scores} />
          </ChartSeries>
          <ChartTooltip render={tooltipRender} />
        </Chart>
      ) : (
        <div className="flex flex-col items-center justify-center text-center h-full py-12">
          <div className="mb-4">
            <svg
              className="mx-auto h-16 w-16 text-text-tertiary"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={1.5}
                d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z"
              />
            </svg>
          </div>
          <h3 className="text-lg font-medium text-text-primary mb-2">
            No Performance Data Available
          </h3>
          <p className="text-text-secondary">
            Start an interview session to see your performance here.
          </p>
        </div>
      )}
    </>
  );
};

export default PerformanceChart;