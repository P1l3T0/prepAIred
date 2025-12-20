import { Card, CardBody, CardHeader } from "@progress/kendo-react-layout";
import { Button } from "@progress/kendo-react-buttons";

interface StartInterviewCardProps {
  isConnected: boolean;
  handleStartConversation: () => void;
  handleEndConversation: () => void;
}

const StartInterviewCard = ({ isConnected, handleStartConversation, handleEndConversation }: StartInterviewCardProps) => {
  return (
    <Card className="shadow-md">
      <CardHeader>
        <h1 className="font-bold text-text-primary">AI Mock Interview</h1>
      </CardHeader>
      <CardBody>
        <div className="flex flex-col md:flex-row md:items-center md:justify-between gap-4">
          <p className="text-text-secondary" style={{ margin: 0 }}>
            Speak naturally, your answers are transcribed in real time.
          </p>

          <div className="flex items-center gap-4">
            <span
              className={`p-2 rounded-xl border ${
                isConnected
                  ? "text-success border-success"
                  : "text-text-tertiary border-border"
              }`}
            >
              {isConnected ? "Live Interview" : "Not Connected"}
            </span>

            <Button
              themeColor={isConnected ? "error" : "primary"}
              onClick={
                isConnected ? handleEndConversation : handleStartConversation
              }
            >
              {isConnected ? "End Interview" : "Start Interview"}
            </Button>
          </div>
        </div>
      </CardBody>
    </Card>
  );
};

export default StartInterviewCard;
