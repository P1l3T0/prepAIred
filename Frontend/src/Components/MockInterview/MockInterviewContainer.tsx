import useMockInterview from "../../Hooks/MockInterview/useMockInterview";
import MockInterviewMessages from "./Cards/MockInterviewMessages";
import StartInterviewCard from "./Cards/StartInterviewCard";

const MockInterviewContainer = () => {
  const {
    isConnected,
    connectionError,
    messages,
    handleStartConversation,
    handleEndConversation,
    handleDropDownChange
  } = useMockInterview();

  return (
    <main className="bg-background">
      <div className="p-6 space-y-4">
        <StartInterviewCard
          isConnected={isConnected}
          connectionError={connectionError}
          handleStartConversation={handleStartConversation}
          handleEndConversation={handleEndConversation}
          handleDropDownChange={handleDropDownChange}
        />

        <MockInterviewMessages messages={messages} />
      </div>
    </main>
  );
};

export default MockInterviewContainer;