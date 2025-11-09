import { Stepper, type StepperChangeEvent, type StepProps } from "@progress/kendo-react-layout";

interface InterviewStepperProps {
  value: number;
  items: StepProps[];
  handleChange: (e: StepperChangeEvent) => void;
}

const InterviewStepper = ({ value, handleChange, items }: InterviewStepperProps) => {
  return (
    <>
      <Stepper value={value} onChange={handleChange} items={items} />
    </>
  );
};

export default InterviewStepper;
