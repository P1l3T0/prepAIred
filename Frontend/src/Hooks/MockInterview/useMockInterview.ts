import { useConversation } from "@elevenlabs/react";
import { useState } from "react";
import type { Message } from "../../Utils/interfaces";

const useMockInterview = () => {
  const [isConnected, setIsConnected] = useState(false);
  const [messages, setMessages] = useState<Message[]>([]);

  const conversation = useConversation({
    onConnect: () => setIsConnected(true),
    onDisconnect: () => {
      setIsConnected(false);
      setMessages([]);
    },
    onError: () => {
      setIsConnected(false);
      setMessages([]);
    },
    onMessage: (message) => {
      if (!message || !message.message || !message.role) return;

      setMessages((prev) => [
        ...prev,
        {
          id: crypto.randomUUID(),
          role: message.role === "agent" ? "ai" : "user",
          message: message.message,
        },
      ]);
    },
  });

  const handleStartConversation = async () => {
    try {
      await navigator.mediaDevices.getUserMedia({ audio: true });
      await conversation.startSession({
        agentId: process.env.ELEVENLABS_AGENT_ID || "",
        connectionType: "webrtc",
      });
    } catch (error) {
      console.error(error);
      setIsConnected(false);
    }
  };

  const handleEndConversation = async () => {
    await conversation.endSession();
  };

  return {
    isConnected,
    messages,
    handleStartConversation,
    handleEndConversation,
  };
};

export default useMockInterview;