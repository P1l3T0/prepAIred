import { RadioButton } from "@progress/kendo-react-inputs";
import type { AnswersProps } from "../../../../Utils/interfaces";

const SingleChoiceAnswers = ({
  interview,
  interviewIndex,
  interviewType,
  onChange,
}: AnswersProps) => (
  <div className="space-y-2">
    {interview.answers?.map((answer, answerIndex) => (
      <div key={answerIndex} className="flex items-center gap-2 mb-2.5">
        <RadioButton
          id={`${interviewType}-single-choice-answer-${interviewIndex}-${answerIndex}`}
          name={`${interviewType}-single-choice-answer-${interviewIndex}`}
          label={answer}
          value={answer}
          disabled={interview.isAnswered}
          defaultChecked={
            interview.selectedAnswer
              ? interview.selectedAnswer === answer
              : undefined
          }
          onChange={(e) => onChange(e.value)}
        />
      </div>
    ))}
  </div>
);

export default SingleChoiceAnswers;
