import { useInterviewStep } from "../../../../Context/InterviewStep/useInterviewStep";
import GenerateHrInterviews from "../../HR/GenerateHrInterviews";
import GetHrInterviews from "../../HR/GetHrInterviews";
import GenerateTechnicalInterviews from "../../Technical/GenerateTechnicalInterviews";
import GetTechnicalInterviews from "../../Technical/GetTechnicalInterviews";
import InterviewSection from "./InterviewSection";
import InterviewStepper from "./InterviewStepper";


const InterviewContainerContent = () => {
  const { value, items } = useInterviewStep();

  return (
    <>
      <main className="bg-background p-3 md:p-6">
        <div className="max-w-7xl mx-auto space-y-12">
          <InterviewStepper value={value} items={items} />

          {value === 0 ? (
            <InterviewSection
              generateInterviews={<GenerateHrInterviews />}
              getInterviews={<GetHrInterviews />}
            />
          ) : (
            <InterviewSection
              generateInterviews={<GenerateTechnicalInterviews />}
              getInterviews={<GetTechnicalInterviews />}
            />
          )}
        </div>
      </main>
    </>
  );
};

export default InterviewContainerContent;