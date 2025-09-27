import type { AnswersProps } from "../../../Utils/interfaces";

const OpenEndedAnswer = ({ interviewIndex, isAnswered }: AnswersProps) => {
  return (
    <textarea
      className="answer"
      name={`hrinterview-${interviewIndex}`}
      placeholder="Type your answer..."
      disabled={isAnswered}
      style={{ width: "100%", height: "100px", resize: "vertical" }}
    />
  );
};

export default OpenEndedAnswer;
