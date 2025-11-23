import { Button } from "@progress/kendo-react-buttons";
import useFinishInterviewSession from "../../../../Hooks/Interviews/Common/useFinishInterviewSession";

const FinishInterviewSessionButton = () => {
  const { handleFinishInterviewSessionClick } = useFinishInterviewSession();

  return (
    <>
      <Button
        onClick={handleFinishInterviewSessionClick}
        themeColor={"primary"}
        fillMode="outline"
        size="large"
        className="w-full"
      >
        <div className="flex items-center">
          <span className="font-bold text-[clamp(1rem,4vw,1.25rem)]">
            Finish Interview Session
          </span>
        </div>
      </Button>
    </>
  );
};

export default FinishInterviewSessionButton;
