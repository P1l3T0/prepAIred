import { Chart, ChartLegend, ChartCategoryAxis, ChartCategoryAxisItem, ChartSeries, ChartSeriesItem, ChartTooltip, type TooltipContext } from "@progress/kendo-react-charts";
import { Loader } from "@progress/kendo-react-indicators";
import { Card, CardBody } from "@progress/kendo-react-layout";
import useGetProgrammingLanguageData from "../../../Hooks/Statistics/Charts/useGetProgrammingLanguageData";

const ProgrammingLanguageChar = () => {
  const { data: programmingLanguageData, isLoading, isError } = useGetProgrammingLanguageData();

  if (isLoading) {
    return (
      <div className="bg-background min-h-full flex items-center justify-center">
        <div className="flex flex-col items-center space-y-4 m-6">
          <Loader size="large" />
          <span className="text-text-secondary">
            Loading Programming Language Data...
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
                Unable to load Programming Language data
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

  const languages: string[] = programmingLanguageData?.map((item) => item.language) || [];
  const sessions: number[] = programmingLanguageData?.map((item) => item.sessions) || [];

  const tooltipRender = (props: TooltipContext) => {
    const value = props.point?.value;
    return `${value} ${value === 1 ? 'session' : 'sessions'}`;
  }

  return (
    <>
      <Chart>
        <ChartLegend visible={false} />
        <ChartCategoryAxis>
          <ChartCategoryAxisItem categories={languages} />
        </ChartCategoryAxis>
        <ChartSeries>
          <ChartSeriesItem type="column" name="Sessions" colorField="color" data={sessions} />
        </ChartSeries>
        <ChartTooltip render={tooltipRender} />
      </Chart>
    </>
  );
}

export default ProgrammingLanguageChar;