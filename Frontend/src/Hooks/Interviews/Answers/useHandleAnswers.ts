/**
 * Custom hook for managing interview answer state and handling user input.
 * Initializes answer arrays based on interview questions and provides handlers for different question types.
 * Automatically categorizes questions by type and sets up appropriate state structures.
 * 
 * @param {HRInterviewDTO[] | TechnicalInterviewDTO[]} interviews - Array of interview questions to manage
 * 
 * @returns {Object} - An object containing:
 *   - openEndedAnswers: Array of open-ended question answers
 *   - handleOpenEndedChange: Handler for text input changes
 *   - singleChoiceAnswers: Array of single choice answers
 *   - handleSingleChoiceChange: Handler for radio button changes
 *   - multipleChoiceAnswers: Array of multiple choice answers (array of selected options)
 *   - handleMultipleChoiceChange: Handler for checkbox changes
 */
import { useState, useEffect } from "react";
import type { Answer, MultipleChoiceAnswer, HRInterviewDTO, TechnicalInterviewDTO } from "../../../Utils/interfaces";

const useHandleAnswers = ( interviews: HRInterviewDTO[] | TechnicalInterviewDTO[]) => {
  const [openEndedAnswers, setOpenEndedAnswers] = useState<Answer[]>([]);
  const [singleChoiceAnswers, setSingleChoiceAnswers] = useState<Answer[]>([]);
  const [multipleChoiceAnswers, setMultipleChoiceAnswers] = useState<MultipleChoiceAnswer[]>([]);

  useEffect(() => {
    if (interviews && interviews.length > 0) {
      setOpenEndedAnswers(
        interviews
          .filter((i) => i.questionType === "OpenEnded")
          .map((i) => ({ question: i.question, answer: "" }))
      );
      setSingleChoiceAnswers(
        interviews
          .filter((i) => i.questionType === "SingleChoice")
          .map((i) => ({ question: i.question, answer: "" }))
      );
      setMultipleChoiceAnswers(
        interviews
          .filter((i) => i.questionType === "MultipleChoice")
          .map((i) => ({ question: i.question, answers: [] }))
      );
    }
  }, [interviews]);

  const handleSingleChoiceChange = (index: number, value: string) => {
    setSingleChoiceAnswers((prev) => {
      return prev.map((item, idx) =>
        idx === index ? { ...item, answer: value } : item
      );
    });
  };

  const handleMultipleChoiceChange = (index: number, value: string) => {
    setMultipleChoiceAnswers((prev) => {
      return prev.map((item, idx) => {
        if (idx === index) {
          const alreadySelected = item.answers.includes(value);
          return {
            ...item,
            answers: alreadySelected
              ? item.answers.filter((v) => v !== value)
              : [...item.answers, value],
          };
        }
        return item;
      });
    });
  };

  const handleOpenEndedChange = (index: number, value: string) => {
    setOpenEndedAnswers((prev) =>
      prev.map((item, idx) =>
        idx === index ? { ...item, answer: value } : item
      )
    );
  };

  return {
    openEndedAnswers,
    handleOpenEndedChange,
    singleChoiceAnswers,
    handleSingleChoiceChange,
    multipleChoiceAnswers,
    handleMultipleChoiceChange,
  };
};

export default useHandleAnswers;