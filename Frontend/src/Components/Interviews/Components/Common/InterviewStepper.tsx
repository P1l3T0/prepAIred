import { Stepper, type StepProps } from "@progress/kendo-react-layout";

interface InterviewStepperProps {
  value: number;
  items: StepProps[];
}

const InterviewStepper = ({ value, items }: InterviewStepperProps) => {
  return (
    <>
      <Stepper value={value} items={items} />
    </>
  );
};

export default InterviewStepper;
