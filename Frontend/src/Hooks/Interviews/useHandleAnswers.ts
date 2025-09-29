import { useState, useEffect } from "react";
import type { Answer, MultipleChoiceAnswer, HRInterviewDTO, TechnicalInterviewDTO } from "../../Utils/interfaces";

const useHandleAnswers = (interviews: HRInterviewDTO[] |TechnicalInterviewDTO[]) => {
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

  const handleOpenEndedChange = (index: number, value: string) => {
    setOpenEndedAnswers((prev) =>
      prev.map((item, idx) =>
        idx === index ? { ...item, answer: value } : item
      )
    );
  };

  const handleSingleChoiceChange = (index: number, value: string) => {
    setSingleChoiceAnswers((prev) => {
      return prev.map((item, idx) =>
        idx === index ? { ...item, answer: value } : item
      );
    });
  };

  const handleMultipleChoiceChange = (index: number, value: string) => {
    setMultipleChoiceAnswers((prev) => {
      return prev.map((item, idx) =>
        idx === index ? { ...item, answers: [...item.answers, value] } : item
      );
    });
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