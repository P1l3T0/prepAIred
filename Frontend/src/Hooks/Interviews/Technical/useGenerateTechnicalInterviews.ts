import axios, { AxiosError } from "axios";
import { useState, type SyntheticEvent } from "react";
import { useMutation, useQueryClient } from "react-query";
import { generateTechnicalInterviewsEndPoint } from "../../../Utils/endpoints";
import type { TechnicalRequestDTO } from "../../../Utils/interfaces";

const useGenerateTechnicalInterviews = () => {
  const queryClient = useQueryClient();

  const [technicalRequest, setTechnicalRequest] = useState<TechnicalRequestDTO>({
      aiAgent: "ChatGPT",
      programmingLanguage: "C#",
      subject: "Object-Oriented Programming",
      difficultyLevel: "Easy",
      position: "Junior Developer",
      numberOfQuestions: 3
    }
  );

  const handleSelectChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setTechnicalRequest({
      ...technicalRequest,
      [e.target.name]: e.target.value,
    });
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setTechnicalRequest({
      ...technicalRequest,
      [e.target.name]: parseInt(e.target.value),
    });
  };

  const generateTechnicalInterviews = async () => {
    await axios
      .post(generateTechnicalInterviewsEndPoint, technicalRequest, { withCredentials: true })
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        alert(error?.title);
      });
  };

  const { mutateAsync } = useMutation({
    mutationFn: generateTechnicalInterviews,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["technical-interviews"] });
    },
  });

  const handleGenerateTechnicalInterviews = async (e: SyntheticEvent) => {
    e.preventDefault();
    mutateAsync();
  };

  return { handleSelectChange, handleInputChange, handleGenerateTechnicalInterviews };
};

export default useGenerateTechnicalInterviews;