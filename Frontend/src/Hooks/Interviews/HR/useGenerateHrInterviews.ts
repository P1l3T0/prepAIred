/**
 * Custom hook for generating HR interviews using AI.
 * Manages form state, handles user input changes, and submits requests to generate interviews.
 * Uses react-query mutations for API calls and cache invalidation.
 *
 * @returns {Object} - An object containing:
 *   - handleDropDownChange: Handler for dropdown and multi-select changes
 *   - handleInputChange: Handler for numeric input changes
 *   - handleGenerateHrInterviews: Handler for form submission
 *   - disabled: Boolean indicating if form should be disabled after submission
 *   - isSubmitting: Boolean indicating if request is in progress
 */
import axios, { AxiosError } from "axios";
import { useState, type SyntheticEvent } from "react";
import { useMutation, useQueryClient } from "react-query";
import { generateHrInterviewsEndPoint } from "../../../Utils/endpoints";
import type { HrRequestDTO } from "../../../Utils/interfaces";
import type {
  DropDownListChangeEvent,
  MultiSelectChangeEvent,
} from "@progress/kendo-react-dropdowns";
import type {
  CheckboxChangeEvent,
  NumericTextBoxChangeEvent,
} from "@progress/kendo-react-inputs";

const useGenerateHrInterviews = () => {
  const queryClient = useQueryClient();
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);

  const [hrRequest, setHrRequest] = useState<HrRequestDTO>({
    aiAgent: "ChatGPT",
    softSkillFocus: ["Communication", "Teamwork"],
    contextScenario: ["Team Collaboration", "Conflict Resolution"],
    numberOfQuestions: 3,
    hasPriorExperience: false,
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
      [name]: e.value,
    });
  };

  const handleCheckBoxChange = (e: CheckboxChangeEvent) => {
    setHrRequest({
      ...hrRequest,
      hasPriorExperience: e.value,
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
    onMutate: () => {
      setIsSubmitting(true);
    },
    onSuccess: () => {
      setIsSubmitting(false);
      queryClient.invalidateQueries({ queryKey: ["hr-interviews"] });
    },
  });

  const handleGenerateHrInterviews = async (e: SyntheticEvent) => {
    e.preventDefault();
    mutateAsync();
  };

  return {
    handleDropDownChange,
    handleInputChange,
    handleGenerateHrInterviews,
    handleCheckBoxChange,
    isSubmitting,
  };
};

export default useGenerateHrInterviews;