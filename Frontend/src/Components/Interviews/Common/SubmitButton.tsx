import { Button } from "@progress/kendo-react-buttons";
import { Loader } from "@progress/kendo-react-indicators";

interface SubmitButtonProps {
  id: string;
  isSubmitting: boolean;
}

const SubmitButton = ({ id, isSubmitting }: SubmitButtonProps) => {
  return (
    <div className="pt-4">
      <Button
        id={id}
        themeColor={"primary"}
        fillMode={"outline"}
        size="large"
        disabled={isSubmitting}
        className="w-full"
        type="submit"
      >
        {isSubmitting ? (
          <>
            <Loader type={"infinite-spinner"} className="mr-2" />
            <span className="font-bold">Generating Intervies...</span>
          </>
        ) : (
          <span className="font-bold">Generate Interview</span>
        )}
      </Button>
    </div>
  );
};

export default SubmitButton;
