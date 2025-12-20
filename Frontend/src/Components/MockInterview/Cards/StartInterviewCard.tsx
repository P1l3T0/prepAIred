import { Card, CardBody } from "@progress/kendo-react-layout";
import { Button } from "@progress/kendo-react-buttons";

interface StartInterviewCardProps {
  isConnected: boolean;
  handleStartConversation: () => void;
  handleEndConversation: () => void;
}

const StartInterviewCard = ({ isConnected, handleStartConversation, handleEndConversation }: StartInterviewCardProps) => {
  return (
    <>
      <Card className="shadow-md">
        <CardBody className="flex flex-col md:flex-row md:items-center md:justify-between gap-4">
          <div>
            <h1 className="text-xl font-semibold text-text-primary">
              AI Mock Technical Interview
            </h1>
            <p className="text-sm text-text-secondary">
              Speak naturally, your answers are transcribed in real time.
            </p>
          </div>

          <div className="flex items-center gap-4">
            <span
              className={`text-sm px-3 py-1 rounded-full border ${
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
        </CardBody>
      </Card>
    </>
  );
};

export default StartInterviewCard;
