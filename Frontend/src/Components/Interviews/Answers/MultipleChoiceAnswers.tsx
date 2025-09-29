import type { AnswersProps } from "../../../Utils/interfaces";

const MultipleChoiceAnswers = ({
  answers,
  interviewIndex,
  isAnswered,
  interviewType,
  onChange
}: AnswersProps) => {
  return (
    <>
      {answers?.map((answer, answerIndex) => (
        <div key={answerIndex} className="answer">
          <input
            type="checkbox"
            id={`${interviewType}-multiple-choice-answer-${interviewIndex}-${answerIndex}`}
            value={answer}
            disabled={isAnswered}
            onChange={(e) => onChange(e.target.value)}
          />
          <label
            htmlFor={`${interviewType}-multiple-choice-answer-${interviewIndex}-${answerIndex}`}
          >
            {answer}
          </label>
        </div>
      ))}
    </>
  );
};

export default MultipleChoiceAnswers;
