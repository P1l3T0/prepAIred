import InterviewGenerateButtonContextProvider from "../../Context/InterviewGenerateButton/InterviewGenerateButtonContext";
import InterviewSessionFinishButtonProvider from "../../Context/InterviewSession/InterviewSessionContext";
import { InterviewStepProvider } from "../../Context/InterviewStep/InterviewStepContext";
import InterviewContainerContent from "./Components/Common/InterviewContainerContent";

const InterviewContainer = () => {
  return (
    <InterviewStepProvider>
      <InterviewSessionFinishButtonProvider>
        <InterviewGenerateButtonContextProvider>
          <InterviewContainerContent />
        </InterviewGenerateButtonContextProvider>
      </InterviewSessionFinishButtonProvider>
    </InterviewStepProvider>
  );
};

export default InterviewContainer;
