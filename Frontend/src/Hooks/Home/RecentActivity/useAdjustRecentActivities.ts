const useAdjustRecentActivities = () => {
  const getScoreColor = (score: number) => {
    if (score >= 8) return "text-success";
    if (score >= 6) return "text-primary";
    if (score >= 4) return "text-warning";

    return "text-error";
  };

  const getStatusColor = (status: string) => {
    switch (status) {
      case "Passed":
        return "text-success";
      case "Failed":
        return "text-error";
    }
  };

  return { getScoreColor, getStatusColor };
};

export default useAdjustRecentActivities;