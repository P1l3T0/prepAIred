import { Button } from "@progress/kendo-react-buttons";
import { Loader } from "@progress/kendo-react-indicators";

interface EvaluationButtonProps {
  disabled: boolean;
  isSubmitting: boolean;
  onClick: (e: React.MouseEvent<HTMLButtonElement>) => void;
}

const EvaluationButton = ({
  onClick,
  isSubmitting,
  disabled,
}: EvaluationButtonProps) => {
  return (
    <Button
      onClick={onClick}
      themeColor={"primary"}
      size="large"
      disabled={disabled}
      className="w-full"
    >
      {isSubmitting ? (
        <>
          <div className="flex items-center">
            <Loader themeColor="light" type={"infinite-spinner"} className="mr-2" />
            <span className="font-bold text-[clamp(1rem,4vw,1.25rem)]">
              Evaluating Interviews...
            </span>
          </div>
        </>
      ) : (
        <div className="flex items-center">
          <span className="font-bold text-[clamp(1rem,4vw,1.25rem)]">
            Evaluate Interview
          </span>
        </div>
      )}
    </Button>
  );
};

export default EvaluationButton;
