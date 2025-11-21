import { Checkbox } from "@progress/kendo-react-inputs";
import type { AnswersProps } from "../../../../Utils/interfaces";

const MultipleChoiceAnswers = ({
  interview,
  interviewIndex,
  interviewType,
  onChange,
}: AnswersProps) => (
  <div className="space-y-2">
    {interview.answers.map((answer, answerIndex) => (
      <div key={answerIndex} className="flex items-center gap-2 mb-2.5">
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
  </div>
);

export default MultipleChoiceAnswers;
