const useAdjustRecentActivities = () => {
  const getScoreColor = (score: number) => {
    if (score >= 8) return "text-primary";
    if (score >= 5) return "text-success";
    if (score >= 3) return "text-warning";

    return "text-error";
  };

  const getStatusColor = (status: string) => {
    if (status === "Passed") return "text-success";
    if (status === "Failed") return "text-error";
  };

  return { getScoreColor, getStatusColor };
};

export default useAdjustRecentActivities;