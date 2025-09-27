import axios, { AxiosError } from "axios";
import { useState, type SyntheticEvent } from "react";
import { useMutation, useQueryClient } from "react-query";
import { generateHrInterviewsEndPoint } from "../../../Utils/endpoints";
import type { HrRequestDTO } from "../../../Utils/interfaces";

const useGenerateHrInterviews = () => {
  const queryClient = useQueryClient();

  const [hrRequest, setHrRequest] = useState<HrRequestDTO>({
    aiAgent: "ChatGPT",
    softSkillFocus: "Communication",
    contextScenario: "Team Collaboration",
    numberOfQuestions: 3
  });

  const handleSelectChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setHrRequest({
      ...hrRequest,
      [e.target.name]: e.target.value,
    });
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setHrRequest({
      ...hrRequest,
      [e.target.name]: parseInt(e.target.value),
    });
  };

  const generateHrInterviews = async () => {
    await axios
      .post(generateHrInterviewsEndPoint, hrRequest, { withCredentials: true })
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        alert(error?.title);
      });
  };

  const { mutateAsync } = useMutation({
    mutationFn: generateHrInterviews,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["hr-interviews"] });
    },
  });

  const handleGenerateHrInterviews = async (e: SyntheticEvent) => {
    e.preventDefault();
    mutateAsync();
  };

  return { handleSelectChange, handleInputChange, handleGenerateHrInterviews };
};

export default useGenerateHrInterviews;