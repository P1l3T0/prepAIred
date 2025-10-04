import axios, { AxiosError } from "axios";
import { useState, type SyntheticEvent } from "react";
import { useMutation, useQueryClient } from "react-query";
import { generateTechnicalInterviewsEndPoint } from "../../../Utils/endpoints";
import type { TechnicalRequestDTO } from "../../../Utils/interfaces";
import type { DropDownListChangeEvent, MultiSelectChangeEvent } from "@progress/kendo-react-dropdowns";
import type { NumericTextBoxChangeEvent } from "@progress/kendo-react-inputs";

const useGenerateTechnicalInterviews = () => {
  const queryClient = useQueryClient();

  const [technicalRequest, setTechnicalRequest] = useState<TechnicalRequestDTO>({
      aiAgent: "ChatGPT",
      programmingLanguage: "C#",
      subject: ["Object-Oriented Programming", "Data Structures and Algorithms"],
      difficultyLevel: "Easy",
      position: "Junior Developer",
      numberOfQuestions: 3
    }
  );

const handleDropDownChange = (e: DropDownListChangeEvent | MultiSelectChangeEvent) => {
    const name: string = e.target.props.name as string;

    setTechnicalRequest({
      ...technicalRequest,
      [name]: e.target.value,
    });
  };

  const handleInputChange = (e: NumericTextBoxChangeEvent) => {
    const name: string = e.target.props.name as string;

    setTechnicalRequest({
      ...technicalRequest,
      [name]: e.value,
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

  return { handleDropDownChange, handleInputChange, handleGenerateTechnicalInterviews };
};

export default useGenerateTechnicalInterviews;