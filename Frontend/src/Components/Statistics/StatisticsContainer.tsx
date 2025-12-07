import StatisticsCard from "./StatisticsCard";
import PerformanceChart from "./Charts/PerformanceChart";
import StatisticsGrid from "./Grid/StatisticsGrid";
import ProgrammingLanguageChar from "./Charts/ProgrammingLanguageChart";
import PositionChart from "./Charts/PositionChart";

const StatisticsContainer = () => {
  return (
    <main className="bg-background">
      <div className="mx-auto p-6">
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-6 mb-6">
          <StatisticsCard title="Interviews by Position">
            <PositionChart />
          </StatisticsCard>
          <StatisticsCard title="Programming Languages Used">
            <ProgrammingLanguageChar />
          </StatisticsCard>
        </div>

        <div className="space-y-6">
          <StatisticsCard title="Performance Over Time">
            <PerformanceChart />
          </StatisticsCard>
          <StatisticsCard title="Detailed Statistics">
            <StatisticsGrid />
          </StatisticsCard>
        </div>
      </div>
    </main>
  );
};

export default StatisticsContainer;
