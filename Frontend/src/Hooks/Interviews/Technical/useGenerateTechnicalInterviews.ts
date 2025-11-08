/**
 * Custom hook for generating technical interviews using AI.
 * Manages form state, handles user input changes, and submits requests to generate interviews.
 * Uses react-query mutations for API calls and cache invalidation.
 *
 * @returns {Object} - An object containing:
 *   - handleDropDownChange: Handler for dropdown and multi-select changes
 *   - handleInputChange: Handler for numeric input changes
 *   - handleGenerateTechnicalInterviews: Handler for form submission
 *   - disabled: Boolean indicating if form should be disabled after submission
 *   - isSubmitting: Boolean indicating if request is in progress
 */
import axios, { AxiosError } from "axios";
import { useState, type SyntheticEvent } from "react";
import { useMutation, useQueryClient } from "react-query";
import { generateTechnicalInterviewsEndPoint } from "../../../Utils/endpoints";
import type { TechnicalRequestDTO } from "../../../Utils/interfaces";
import type {
  DropDownListChangeEvent,
  MultiSelectChangeEvent,
} from "@progress/kendo-react-dropdowns";
import type { CheckboxChangeEvent, NumericTextBoxChangeEvent } from "@progress/kendo-react-inputs";

const useGenerateTechnicalInterviews = () => {
  const queryClient = useQueryClient();
  const [isSubmitting, setIsSubmitting] = useState(false);

  const [technicalRequest, setTechnicalRequest] = useState<TechnicalRequestDTO>({
    aiAgent: "ChatGPT",
    programmingLanguage: "C#",
    subject: [
      "Object-Oriented Programming",
      "Data Structures and Algorithms",
    ],
    difficultyLevel: "Easy",
    position: "Junior Developer",
    numberOfQuestions: 3,
    hasPriorExperience: false,
  });

  const handleDropDownChange = (
    e: DropDownListChangeEvent | MultiSelectChangeEvent
  ) => {
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

  const handleCheckBoxChange = (e: CheckboxChangeEvent) => {
    setTechnicalRequest({
      ...technicalRequest,
      hasPriorExperience: e.value,
    });
  }

  const generateTechnicalInterviews = async () => {
    await axios
      .post(generateTechnicalInterviewsEndPoint, technicalRequest, {
        withCredentials: true,
      })
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        alert(error?.title);
      });
  };

  const { mutateAsync } = useMutation({
    mutationFn: generateTechnicalInterviews,
    onMutate: () => {
      setIsSubmitting(true);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["technical-interviews"] });
      setIsSubmitting(false);
    },
  });

  const handleGenerateTechnicalInterviews = async (e: SyntheticEvent) => {
    e.preventDefault();
    mutateAsync();
  };

  return {
    handleDropDownChange,
    handleInputChange,
    handleGenerateTechnicalInterviews,
    handleCheckBoxChange,
    isSubmitting,
  };
};

export default useGenerateTechnicalInterviews;
