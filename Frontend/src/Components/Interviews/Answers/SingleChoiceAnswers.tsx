import type { AnswersProps } from "../../../Utils/interfaces";

const SingleChoiceAnswers = ({
  answers,
  interviewIndex,
  isAnswered,
  interviewType,
  onChange,
}: AnswersProps) => (
  <>
    {answers?.map((answer, answerIndex) => (
      <div key={answerIndex} className="answer">
        <input
          type="radio"
          id={`${interviewType}-single-choice-answer-${interviewIndex}-${answerIndex}`}
          value={answer}
          disabled={isAnswered}
          onChange={(e) => onChange(e.target.value)}
        />
        <label
          htmlFor={`${interviewType}-single-choice-answer-${interviewIndex}-${answerIndex}`}
        >
          {answer}
        </label>
      </div>
    ))}
  </>
);

export default SingleChoiceAnswers;
