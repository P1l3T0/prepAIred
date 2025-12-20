import StatisticCard from "./StatisticCard";
import useProcessStatistics from "../../../../Hooks/InterviewSessions/useProcessStatistics";
import useGetInterviewSessionStatistics from "../../../../Hooks/InterviewSessions/useGetInterviewSessionStatistics";
import type { ProfileStats } from "../../../../Utils/interfaces";
import { Loader } from "@progress/kendo-react-indicators";
import { Card, CardBody } from "@progress/kendo-react-layout";

const StatisticsGrid = () => {
  const { data: interviewSessionStatistics, isLoading, isError} = useGetInterviewSessionStatistics();

  if (isLoading) {
    return (
      <div className="min-h-[calc(100vh-4.05rem)] sm:min-h-[calc(100vh-4.55rem)] bg-background flex items-center justify-center">
        <div className="flex flex-col items-center space-y-4">
          <Loader size="large" />
          <span className="text-text-secondary">Loading your profile...</span>
        </div>
      </div>
    );
  }

  if (isError) {
    return (
      <div className="min-h-[calc(100vh-4.05rem)] sm:min-h-[calc(100vh-4.55rem)] bg-background flex items-center justify-center">
        <Card className="shadow-lg">
          <CardBody>
            <div className="text-center p-8">
              <h2 className="text-xl text-text-primary font-semibold mb-2">
                Unable to load profile
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
  } = useProcessStatistics(interviewSessionStatistics as ProfileStats);

  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
      <StatisticCard
        title={totalInterviewSessions === 1 ? "Interview Session" : "Interview Sessions"}
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