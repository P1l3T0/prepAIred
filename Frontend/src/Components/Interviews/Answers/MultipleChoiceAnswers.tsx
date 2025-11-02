import type { AnswersProps } from "../../../Utils/interfaces";
import { Checkbox } from "@progress/kendo-react-inputs";

const MultipleChoiceAnswers = ({
  interview,
  interviewIndex,
  interviewType,
  onChange,
}: AnswersProps) => (
  <>
    {interview.answers.map((answer, answerIndex) => (
      <div key={answerIndex} className="answer">
        <Checkbox
          id={`${interviewType}-multiple-choice-answer-${interviewIndex}-${answerIndex}`}
          label={answer}
          value={answer}
          disabled={interview.isAnswered}
          defaultChecked={interview.selectedAnswer.includes(answer)}
          onChange={(e) => onChange(e.target.element?.value!)}
        />
      </div>
    ))}
  </>
);

export default MultipleChoiceAnswers;
