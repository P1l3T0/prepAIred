import { Card, CardBody, CardHeader } from "@progress/kendo-react-layout";
import type { Message } from "../../../Utils/interfaces";

interface MockInterviewMessagesProps {
  messages: Message[];
  isConnected: boolean;
  isListening: boolean;
}

const MockInterviewMessages = ({ messages, isConnected, isListening }: MockInterviewMessagesProps) => {
  return (
    <>
      <Card className="shadow-md min-h-[550px]">
        <CardHeader>
          <h2 className="text-lg font-medium text-text-primary">
            Interview Conversation
          </h2>
        </CardHeader>

        <CardBody>
          <div className="overflow-y-auto rounded-md border border-border-subtle bg-surface p-4 space-y-4">
            {messages.length === 0 && (
              <p className="text-sm text-text-tertiary italic">
                The interview will begin once you start the session.
              </p>
            )}

            {messages.map((msg) => (
              <div
                key={msg.id}
                className={`p-3 rounded-md ${
                  msg.role === "ai"
                    ? "bg-elevated text-text-secondary"
                    : "bg-primary/10 text-text-primary"
                }`}
              >
                <p className="text-sm" style={{ margin: 0 }}>
                  <span className="font-bold">
                    {msg.role === "ai" ? "AI:" : "You:"}
                  </span>{" "}
                  {msg.message}
                </p>
              </div>
            ))}

            {isConnected && isListening && (
              <div className="text-sm text-text-tertiary italic">
                Listening for your response…
              </div>
            )}
          </div>
        </CardBody>
      </Card>
    </>
  );
};

export default MockInterviewMessages;
