import StatisticCard from "./StatisticCard";
import type { ProfileStats } from "../../../../Utils/interfaces";
import useProcessStatistics from "../../../../Hooks/InterviewSessions/useProcessStatistics";

interface StatisticsGridProps {
  profileStats: ProfileStats;
}

const StatisticsGrid = ({ profileStats }: StatisticsGridProps) => {
  const {
    interviewSessionGoal,
    totalInterviewSessions,
    totalInterviewSessionsProgress,
    passedInterviewSessions,
    passedInterviewSessionsProgress,
    passedInterviewsSubtitle,
    averageScoreProgress,
    averageScoreSubtitle,
    ongoingInterviewSessions,
    ongoingInterviewSubtitle,
  } = useProcessStatistics(profileStats);

  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
      <StatisticCard
        title={totalInterviewSessions > 1 ? "Total Interview Sessions" : "Total Interview Session"}
        themeColor="primary"
        subtitle={`Goal: ${interviewSessionGoal}`}
        value={totalInterviewSessions}
        progressValue={totalInterviewSessionsProgress}
      />

      <StatisticCard
        title="Passed Interview Sessions"
        themeColor="secondary"
        subtitle={passedInterviewsSubtitle}
        value={passedInterviewSessions}
        progressValue={passedInterviewSessionsProgress}
      />

      <StatisticCard
        title="Average Score"
        themeColor="success"
        subtitle={averageScoreSubtitle}
        value={averageScoreProgress}
      />

      <StatisticCard
        title="Ongoing Interview Session"
        themeColor="warning"
        subtitle={ongoingInterviewSubtitle}
        value={ongoingInterviewSessions}
      />
    </div>
  );
};

export default StatisticsGrid;