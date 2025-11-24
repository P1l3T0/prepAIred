import { useContext } from "react";
import { InterviewGenerateButtonContext } from "./InterviewGenerateButtonContext";

const useInterviewGenerateButton = () => {
  const context = useContext(InterviewGenerateButtonContext);
  if (!context) throw new Error("useInterviewGenerateButton must be used within an InterviewGenerateButtonProvider");

  return context;
};

export default useInterviewGenerateButton;
