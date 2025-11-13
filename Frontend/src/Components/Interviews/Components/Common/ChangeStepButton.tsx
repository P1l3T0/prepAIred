import { Button } from "@progress/kendo-react-buttons";

interface ChangeStepButtonProps {
  label: string;
  disabled: boolean;
  onClick: () => void;
}

const ChangeStepButton = ({ label, disabled, onClick }: ChangeStepButtonProps) => {
  return (
    <>
      <Button
        onClick={onClick}
        themeColor={"tertiary"}
        fillMode="outline"
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
