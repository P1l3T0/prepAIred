import type { AnswersProps } from "../../../Utils/interfaces";
import { TextArea } from "@progress/kendo-react-inputs";

const OpenEndedAnswer = ({
  interview,
  interviewIndex,
  interviewType,
  onChange,
}: AnswersProps) => (
  <TextArea
    id={`${interviewType}-open-ended-answer-${interviewIndex}`}
    placeholder={"Type your answer..."}
    disabled={interview.isAnswered}
    onChange={(e) => onChange(e.value)}
    defaultValue={interview.selectedAnswer || ""}
    style={{ width: "100%", height: "100px", resize: "vertical" }}
  />
);

export default OpenEndedAnswer;
