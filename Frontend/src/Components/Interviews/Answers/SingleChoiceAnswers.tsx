import type { AnswersProps } from "../../../Utils/interfaces";

const SingleChoiceAnswers = ({
  answers,
  interviewIndex,
  isAnswered,
  interviewType,
}: AnswersProps) => (
  <>
    {answers?.map((answer, answerIndex) => (
      <div key={answerIndex} className="answer">
        <input
          type="radio"
          id={`${interviewType}-questions-${interviewIndex}-${answerIndex}`}
          name={`${interviewType}-${interviewIndex}`}
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

export default SingleChoiceAnswers;
