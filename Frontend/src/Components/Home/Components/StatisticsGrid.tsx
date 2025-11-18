import type { ProfileStats } from '../../../Utils/interfaces';
import StatisticCard from './StatisticCard';

interface StatisticsGridProps {
  profileStats: Pick<ProfileStats, 'totalInterviews' | 'passedInterviews' | 'ongoingInterviews' | 'averageScore'>;
}

const StatisticsGrid = ({ profileStats }: StatisticsGridProps) => {
  return (
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
      <StatisticCard
        title="Total Interviews"
        value={profileStats.totalInterviews}
        themeColor="primary"
        progressValue="85%"
        subtitle="Goal: 30"
      />

      <StatisticCard
        title="Passed Interviews"
        value={profileStats.passedInterviews}
        themeColor="secondary"
        progressValue={`${
          (profileStats.passedInterviews / profileStats.totalInterviews) * 100
        }%`}
        subtitle={`${Math.round(
          (profileStats.passedInterviews / profileStats.totalInterviews) * 100
        )}% Success Rate`}
      />

      <StatisticCard
        title="Average Score"
        value={`${profileStats.averageScore}%`}
        themeColor="success"
        progressValue={`${profileStats.averageScore}%`}
        subtitle={
          profileStats.averageScore >= 90
            ? "Excellent"
            : profileStats.averageScore >= 75
              ? "Good"
              : "Needs Improvement"
        }
      />

      <StatisticCard
        title="Ongoing Interviews"
        value={profileStats.ongoingInterviews}
        themeColor="warning"
        subtitle="Continue the good work!"
      />
    </div>
  );
};

export default StatisticsGrid;