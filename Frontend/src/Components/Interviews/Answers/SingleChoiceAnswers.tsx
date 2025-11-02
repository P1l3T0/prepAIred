import type { AnswersProps } from "../../../Utils/interfaces";
import { RadioButton } from "@progress/kendo-react-inputs";

const SingleChoiceAnswers = ({
  interview,
  interviewIndex,
  interviewType,
  onChange,
}: AnswersProps) => (
  <>
    {interview.answers?.map((answer, answerIndex) => (
      <div key={answerIndex} className="answer">
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
  </>
);

export default SingleChoiceAnswers;
