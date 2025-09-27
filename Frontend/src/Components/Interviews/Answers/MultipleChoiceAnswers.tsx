import type { AnswersProps } from "../../../Utils/interfaces";

const MultipleChoiceAnswers = ({
  answers,
  interviewIndex,
  isAnswered,
  interviewType,
}: AnswersProps) => {
  return (
    <>
      {answers?.map((answer, answerIndex) => (
        <div key={answerIndex} className="answer">
          <input
            type="checkbox"
            id={`${interviewType}-questions-${interviewIndex}-${answerIndex}`}
            name={`${interviewType}-questions-${interviewIndex}`}
            value={answer}
            disabled={isAnswered}
          />
          <label
            htmlFor={`${interviewType}-questions-${interviewIndex}-${answerIndex}`}
          >
            {answer}
          </label>
        </div>
      ))}
    </>
  );
};

export default MultipleChoiceAnswers;
