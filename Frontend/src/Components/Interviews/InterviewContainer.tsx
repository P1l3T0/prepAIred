import InterviewSection from "./Components/Common/InterviewSection";
import GenerateHrInterviews from "./HR/GenerateHrInterviews";
import GetHrInterviews from "./HR/GetHrInterviews";
import GenerateTechnicalInterviews from "./Technical/GenerateTechnicalInterviews";
import GetTechnicalInterviews from "./Technical/GetTechnicalInterviews";
import InterviewStepper from "./Components/Common/InterviewStepper";
import useChangeStep from "../../Hooks/Interviews/Common/useChangeStep";

const InterviewContainer = () => {
  const { value, items, handleChangeStep } = useChangeStep();

  return (
    <>
      <div className="bg-linear-to-br from-background via-background to-primary p-6">
        <div className="max-w-7xl mx-auto space-y-12">
          <div className="text-center mb-8">
            <h1 className="text-[clamp(1.5rem,4vw,2.5rem)] font-bold text-text-primary mb-3">
              Interview Preparation Hub
            </h1>
            <p className="text-[clamp(1rem,2vw,1.25rem)] text-text-secondary max-w-2xl mx-auto">
              Generate AI-powered interviews and review your practice sessions
            </p>
          </div>

          <InterviewStepper value={value} items={items} />

          {value === 0 ? (
            <InterviewSection
              generateInterviews={<GenerateHrInterviews />}
              getInterviews={
                <GetHrInterviews handleChangeStep={handleChangeStep} />
              }
            />
          ) : (
            <InterviewSection
              generateInterviews={<GenerateTechnicalInterviews />}
              getInterviews={
                <GetTechnicalInterviews handleChangeStep={handleChangeStep} />
              }
            />
          )}
        </div>
      </div>
    </>
  );
};

export default InterviewContainer;
