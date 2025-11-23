/**
 * Custom hook for processing and calculating statistics display values
 * @param profileStats - The raw profile statistics data
 * @returns {Object} - Processed statistics values ready for display
 */
import type { ProfileStats } from "../../Utils/interfaces";

interface ProcessedStatistics {
  totalInterviewSessions: number;
  totalInterviewSessionsProgress: string;
  passedInterviewSessions: number;
  passedInterviewSessionsProgress: string;
  passedInterviewsSubtitle: string;
  averageScore: number;
  averageScoreProgress: string;
  averageScoreSubtitle: string;
  ongoingInterviewSessions: number;
  ongoingInterviewSubtitle: string;
}

const useProcessStatistics = (profileStats: ProfileStats): ProcessedStatistics => {
  const totalInterviewSessions: number = profileStats.totalInterviewSessions || 0;
  const totalInterviewSessionsProgress: string = `${(totalInterviewSessions / 10) * 100 }%`;

  const passedInterviewSessions: number = profileStats.passedInterviewSessions || 0;
  const passedInterviewSessionsProgress: string = totalInterviewSessions > 0 ? `${(profileStats.completionRate)}%` : "0%";
  const passedInterviewsSubtitle: string = `${passedInterviewSessions} out of ${totalInterviewSessions}`;

  const averageScore = profileStats.averageScore || 0;
  const averageScoreProgress: string = `${Math.floor(averageScore * 100) / 100}`;
  const averageScoreSubtitle =
    averageScore >= 7
      ? "Excellent"
      : averageScore > 5
      ? "Good"
      : averageScore === 0
      ? "No Attempts Yet"
      : "Needs Improvement";

  const ongoingInterviewSessions: number = profileStats.ongoingInterviewSessions || 0;
  const ongoingInterviewSubtitle: string = ongoingInterviewSessions !== 0 
    ? `Continue the great work!` 
    : "Start a Session Now!";

  return {
    totalInterviewSessions,
    totalInterviewSessionsProgress,
    passedInterviewSessions,
    passedInterviewSessionsProgress,
    passedInterviewsSubtitle,
    averageScore,
    averageScoreProgress,
    averageScoreSubtitle,
    ongoingInterviewSessions,
    ongoingInterviewSubtitle,
  };
};

export default useProcessStatistics;