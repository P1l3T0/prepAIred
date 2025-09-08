/**
 * Custom hook for handling user logout functionality.
 * Sends logout request, updates auth context, and navigates to login page.
 * @returns {Object} - handleLogOut function
 */
import axios, { AxiosError } from "axios";
import { useState, type SyntheticEvent } from "react";
import { useMutation, useQueryClient } from "react-query";
import { generateInterviewsEndPoint } from "../../Utils/endpoints";
import type { AIRequestDTO } from "../../Utils/interviewTypes";

const useGetInterviewSessions = () => {
  const queryClient = useQueryClient();

  const [aiRequest, setAIRequest] = useState<AIRequestDTO>({
    aiAgent: "ChatGPT",
    topic: "OOP",
    level: "Entry Level",
    numberOfQuestions: 3,
  });

  const handleSelectChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setAIRequest({
      ...aiRequest,
      [e.target.name]: e.target.value,
    });
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setAIRequest({
      ...aiRequest,
      [e.target.name]: parseInt(e.target.value),
    });
  };

  const generateInterviewSessions = async () => {
    await axios
      .post(generateInterviewsEndPoint, aiRequest, { withCredentials: true })
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        console.error(error?.title);
      });
  };

  const { mutateAsync } = useMutation({
    mutationFn: generateInterviewSessions,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["interview-sessions"] });
    },
  });

  const handleGenerateInterviewSessions = async (e: SyntheticEvent) => {
    e.preventDefault();
    mutateAsync();
  };

  return { handleSelectChange, handleInputChange, handleGenerateInterviewSessions };
};

export default useGetInterviewSessions;