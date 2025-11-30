const useGetProperColor = (themeColor: "primary" | "secondary" | "success" | "warning") => {
  const getTextColor = (): string => {
    switch (themeColor) {
      case "primary":
        return "text-primary";
      case "secondary":
        return "text-secondary";
      case "success":
        return "text-success";
      case "warning":
        return "text-warning";
      default:
        return "text-primary";
    }
  };

  const getProgressColor = (): string => {
    switch (themeColor) {
      case "primary":
        return "#3b82f6";
      case "secondary":
        return "#a78bfa";
      case "success":
        return "#34d399";
      case "warning":
        return "#fbbf24";
      default:
        return "#8b5cf6";
    }
  };

  return { getTextColor, getProgressColor };
};

export default useGetProperColor;
