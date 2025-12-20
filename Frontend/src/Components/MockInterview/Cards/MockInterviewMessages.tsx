import { Card, CardBody, CardHeader } from "@progress/kendo-react-layout";
import type { Message } from "../../../Utils/interfaces";
import useAuth from "../../../Context/Auth/useAuth";

interface MockInterviewMessagesProps {
  messages: Message[];
}

const MockInterviewMessages = ({ messages }: MockInterviewMessagesProps) => {
  const { auth } = useAuth();

  return (
    <>
      <Card className="shadow-md min-h-[550px]">
        <CardHeader>
          <h2 className="text-lg font-medium text-text-primary">
            Interview Conversation
          </h2>
        </CardHeader>

        <CardBody>
          <div className="overflow-y-auto space-y-4 max-h-[450px] ">
            {messages.length === 0 && (
              <p className="text-text-tertiary italic bg-primary/10 p-3 rounded-md">
                The interview will begin once you start the session.
              </p>
            )}

            {messages.map((message: Message) => (
              <div
                key={message.id}
                className={`p-3 rounded-md ${
                  message.role === "ai"
                    ? "bg-elevated text-text-secondary"
                    : "bg-primary/10 text-text-primary"
                }`}
              >
                <p className="text-sm" style={{ margin: 0 }}>
                  <span className="font-bold">
                    {message.role === "ai" ? "AI:" : `${auth?.username}:`}
                  </span>{" "}
                  {message.message}
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
