import type { AnswersProps } from "../../../Utils/interfaces";
import { Checkbox } from '@progress/kendo-react-inputs';

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
          <Checkbox
            id={`${interviewType}-multiple-choice-answer-${interviewIndex}-${answerIndex}`}
            label={answer}
            value={answer}
            disabled={isAnswered}
            onChange={(e) => onChange(e.target.element?.value!)}
          />
        </div>
      ))}
    </>
  );
};

export default MultipleChoiceAnswers;
