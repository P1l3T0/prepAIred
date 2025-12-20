import useMockInterview from "../../Hooks/MockInterview/useMockInterview";
import MockInterviewMessages from "./Cards/MockInterviewMessages";
import StartInterviewCard from "./Cards/StartInterviewCard";

const MockInterviewContainer = () => {
  const {
    isConnected,
    messages,
    handleStartConversation,
    handleEndConversation,
  } = useMockInterview();

  return (
    <main className="bg-background">
      <div className="p-6 space-y-6">
        <StartInterviewCard
          isConnected={isConnected}
          handleStartConversation={handleStartConversation}
          handleEndConversation={handleEndConversation}
        />

        <MockInterviewMessages messages={messages} />
      </div>
    </main>
  );
};

export default MockInterviewContainer;
