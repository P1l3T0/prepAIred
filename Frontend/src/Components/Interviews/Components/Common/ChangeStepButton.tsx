import { Button } from "@progress/kendo-react-buttons";
import { useInterviewStep } from "../../../../Context/InterviewStep/useInterviewStep";
interface ChangeStepButtonProps {
  label: string;
  disabled: boolean;
}

const ChangeStepButton = ({ label, disabled }: ChangeStepButtonProps) => {
  const { handleChangeStep } = useInterviewStep();

  return (
    <>
      <Button
        onClick={() => handleChangeStep()}
        themeColor={"tertiary"}
        size="large"
        disabled={disabled}
        className="w-full"
      >
        <div className="flex items-center">
          <span className="font-bold text-[clamp(1rem,4vw,1.25rem)]">
            {label}
          </span>
        </div>
      </Button>
    </>
  );
};

export default ChangeStepButton;
