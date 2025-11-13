import { Button } from "@progress/kendo-react-buttons";
import { Loader } from "@progress/kendo-react-indicators";

interface GenerateButtonProps {
  isSubmitting: boolean;
  interviewType: "HR" | "Technical";
}

const GenerateButton = ({
  isSubmitting,
  interviewType,
}: GenerateButtonProps) => {
  return (
    <div className="pt-4">
      <Button
        themeColor={"primary"}
        fillMode={"outline"}
        size="large"
        disabled={isSubmitting}
        className="w-full"
        type="submit"
      >
        {isSubmitting ? (
          <>
            <div className="flex items-center ">
              <Loader type={"infinite-spinner"} className="mr-2" />
              <span className="font-bold text-[clamp(1rem,4vw,1.25rem)]">
                Generating {interviewType} interview...
              </span>
            </div>
          </>
        ) : (
          <div className="flex items-center">
            <span className="font-bold text-[clamp(1rem,4vw,1.25rem)]">
              Generate {interviewType} Interview
            </span>
          </div>
        )}
      </Button>
    </div>
  );
};

export default GenerateButton;
