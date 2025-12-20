import { useConversation } from "@elevenlabs/react";
import { useState } from "react";
import type { Message } from "../../Utils/interfaces";

const useMockInterview = () => {
  const [isConnected, setIsConnected] = useState(false);
  const [isListening, setIsListening] = useState(false);
  const [messages, setMessages] = useState<Message[]>([]);

  const conversation = useConversation({
    onListening: () => setIsListening(true),
    onThinking: () => setIsListening(false),
    onConnect: () => setIsConnected(true),
    onDisconnect: () => {
      setIsConnected(false);
      setIsListening(false);
    },
    onError: (error) => {
      console.error("ElevenLabs error:", error);
      setIsConnected(false);
      setIsListening(false);
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
        agentId: "agent_0801kckpbq8mew08w452mmv264zv",
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
    isListening,
    messages,
    handleStartConversation,
    handleEndConversation,
  };
};

export default useMockInterview;