import axios, { AxiosError } from "axios";
import { useState, type SyntheticEvent } from "react";
import { useMutation, useQueryClient } from "react-query";
import { generateHrInterviewsEndPoint } from "../../../Utils/endpoints";
import type { HrRequestDTO } from "../../../Utils/interfaces";
import type { DropDownListChangeEvent, MultiSelectChangeEvent } from "@progress/kendo-react-dropdowns";
import type { NumericTextBoxChangeEvent } from "@progress/kendo-react-inputs";

const useGenerateHrInterviews = () => {
  const queryClient = useQueryClient();

  const [hrRequest, setHrRequest] = useState<HrRequestDTO>({
    aiAgent: "ChatGPT",
    softSkillFocus: ["Communication", "Teamwork"],
    contextScenario: "Team Collaboration",
    numberOfQuestions: 3
  });

  const handleDropDownChange = (e: DropDownListChangeEvent | MultiSelectChangeEvent) => {
    const name: string = e.target.props.name as string;

    setHrRequest({
      ...hrRequest,
      [name]: e.target.value,
    });
  };

  const handleInputChange = (e: NumericTextBoxChangeEvent) => {
    const name: string = e.target.props.name as string;

    setHrRequest({
      ...hrRequest,
      [name]: e.value
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

  return { handleDropDownChange, handleInputChange, handleGenerateHrInterviews };
};

export default useGenerateHrInterviews;