import type { AnswersProps } from "../../../Utils/interfaces";

const MultipleChoiceAnswers = ({ answers, interviewIndex, isAnswered }: AnswersProps) => {
  return (
    <>
      {answers?.map((answer, answerIndex) => (
        <div key={answerIndex} className="answer">
          <input
            type="checkbox"
            id={`hrinterview-questions-${interviewIndex}-${answerIndex}`}
            name={`hrinterview-${interviewIndex}`}
            value={answer}
            disabled={isAnswered}
          />
          <label
            htmlFor={`hrinterview-questions-${interviewIndex}-${answerIndex}`}
          >
            {answer}
          </label>
        </div>
      ))}
    </>
  );
};

export default MultipleChoiceAnswers;
