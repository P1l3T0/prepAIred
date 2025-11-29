import { useContext } from "react";
import { InterviewSessionContext } from "./InterviewSessionContext";

const useInterviewSession = () => {
  const context = useContext(InterviewSessionContext);
  if (!context) throw new Error("useInterviewSession must be used within an InterviewSessionProvider");

  return context;
};

export default useInterviewSession;
