import { Card, CardBody, CardHeader } from "@progress/kendo-react-layout";
import type { Message } from "../../../Utils/interfaces";

interface MockInterviewMessagesProps {
  messages: Message[];
}

const MockInterviewMessages = ({ messages }: MockInterviewMessagesProps) => {
  return (
    <>
      <Card className="shadow-md min-h-[550px]">
        <CardHeader>
          <h2 className="text-lg font-medium text-text-primary">
            Interview Conversation
          </h2>
        </CardHeader>

        <CardBody>
          <div className="overflow-y-auto px-2 space-y-4 max-h-[450px] ">
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
          </div>
        </CardBody>
      </Card>
    </>
  );
};

export default MockInterviewMessages;
