import { Chart, ChartLegend, ChartCategoryAxis, ChartCategoryAxisItem, ChartSeries, ChartSeriesItem, ChartTooltip } from "@progress/kendo-react-charts";
import { Loader } from "@progress/kendo-react-indicators";
import { Card, CardBody } from "@progress/kendo-react-layout";
import useGetPerformanceData from "../../../Hooks/Statistics/Charts/useGetPerformanceData";

const PerformanceChart = () => {
  const { data: performanceData, isLoading, isError } = useGetPerformanceData();

  if (isLoading) {
    return (
      <div className="bg-background flex items-center justify-center">
        <div className="flex flex-col items-center space-y-4 m-6">
          <Loader size="large" />
          <span className="text-text-secondary">
            Loading Performance Data...
          </span>
        </div>
      </div>
    );
  }

  if (isError) {
    return (
      <div className="bg-background flex items-center justify-center">
        <Card className="shadow-lg w-full m-6">
          <CardBody>
            <div className="text-center p-8">
              <h2 className="text-xl text-text-primary font-semibold mb-2">
                Unable to load Performance data
              </h2>
              <p className="text-text-secondary">
                Please try refreshing the page
              </p>
            </div>
          </CardBody>
        </Card>
      </div>
    );
  }

  const scores: number[] = performanceData?.map((data) => data.score) || [];

  const categories: string[] = performanceData?.map((data) =>
    new Date(data.dateCreated).toLocaleDateString("en-US", {
      day: "numeric",
      month: "short",
      hour: "2-digit",
      minute: "2-digit",
    })
  ) || [];

  return (
    <>
      <Chart>
        <ChartLegend visible={false} />
        <ChartCategoryAxis>
          <ChartCategoryAxisItem categories={categories} />
        </ChartCategoryAxis>
        <ChartSeries>
          <ChartSeriesItem type="line" name="Score" style="smooth" data={scores} />
        </ChartSeries>
        <ChartTooltip />
      </Chart>
    </>
  );
};

export default PerformanceChart;
