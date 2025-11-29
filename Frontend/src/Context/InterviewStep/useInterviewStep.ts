import { useContext } from "react";
import { InterviewStepContext } from "./InterviewStepContext";

export const useInterviewStep = () => {
  const context = useContext(InterviewStepContext);
  if (!context) throw new Error("useInterviewStep must be used within an InterviewStepProvider");

  return context;
};
