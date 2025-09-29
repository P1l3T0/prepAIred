import type { AnswersProps } from "../../../Utils/interfaces";

const OpenEndedAnswer = ({
  interviewIndex,
  isAnswered,
  interviewType,
  onChange,
}: AnswersProps) => {
  return (
    <textarea
      id={`${interviewType}-open-ended-answer-${interviewIndex}`}
      placeholder={"Type your answer..."}
      disabled={isAnswered}
      onChange={e => onChange(e.target.value)}
      style={{ width: "100%", height: "100px", resize: "vertical" }}
    />
  );
};

export default OpenEndedAnswer;
