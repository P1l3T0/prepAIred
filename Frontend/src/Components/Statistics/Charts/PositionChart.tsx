import { Chart, ChartSeries, ChartSeriesItem, ChartLegend, ChartTooltip, type TooltipContext } from "@progress/kendo-react-charts";
import { Loader } from "@progress/kendo-react-indicators";
import useGetPositionData from "../../../Hooks/Statistics/Charts/useGetPositionData";
import { Card, CardBody } from "@progress/kendo-react-layout";
``
const PositionChart = () => {
  const { data: positionData, isLoading, isError } = useGetPositionData();

  const tooltipRender = (props: TooltipContext) => {
    const { category, value } = props.point || {};
    return `${category}: ${value} ${value === 1 ? 'session' : 'sessions'}`;
  }

  if (isLoading) {
    return (
      <div className="bg-background min-h-full flex items-center justify-center">
        <div className="flex flex-col items-center space-y-4 m-6">
          <Loader size="large" />
          <span className="text-text-secondary">
            Loading Position Data...
          </span>
        </div>
      </div>
    );
  }

  if (isError) {
    return (
      <div className="bg-background flex items-center min-h-full justify-center">
        <Card className="shadow-lg w-full m-6">
          <CardBody>
            <div className="text-center p-8">
              <h2 className="text-xl text-text-primary font-semibold mb-2">
                Unable to load Position data
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

  return (
    <>
    {positionData && positionData.length > 0 ? (
      <Chart>
        <ChartLegend position="bottom" />
        <ChartSeries>
          <ChartSeriesItem type="donut" categoryField="position" field="sessions" data={positionData} />
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
          No Position Data Available
        </h3>
        <p className="text-text-secondary">
          Start an interview session to see your activities here.
        </p>
      </div>
    )}
    </>
  );
};

export default PositionChart;
