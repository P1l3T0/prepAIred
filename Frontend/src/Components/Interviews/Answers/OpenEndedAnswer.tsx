import type { AnswersProps } from "../../../Utils/interfaces";
import { TextArea } from "@progress/kendo-react-inputs";

const OpenEndedAnswer = ({
  interviewIndex,
  isAnswered,
  interviewType,
  onChange,
}: AnswersProps) => {
  return (
    <TextArea
      id={`${interviewType}-open-ended-answer-${interviewIndex}`}
      placeholder={"Type your answer..."}
      disabled={isAnswered}
      onChange={(e) => onChange(e.value)}
      style={{ width: "100%", height: "100px", resize: "vertical" }}
    />
  );
};

export default OpenEndedAnswer;
