import type { AnswersProps } from "../../../Utils/interfaces";
import { RadioButton } from "@progress/kendo-react-inputs";

const SingleChoiceAnswers = ({
  answers,
  interviewIndex,
  isAnswered,
  interviewType,
  onChange,
}: AnswersProps) => {
  return (
    <>
      {answers?.map((answer, answerIndex) => (
        <div key={answerIndex} className="answer">
          <RadioButton
            id={`${interviewType}-single-choice-answer-${interviewIndex}-${answerIndex}`}
            name={`${interviewType}-single-choice-answer-${interviewIndex}`}
            label={answer}
            value={answer}
            disabled={isAnswered}
            onChange={(e) => onChange(e.value)}
          />
        </div>
      ))}
    </>
  );
};

export default SingleChoiceAnswers;
