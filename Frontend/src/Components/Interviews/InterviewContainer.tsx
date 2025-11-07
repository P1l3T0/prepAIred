import InterviewSection from "./Components/Common/IntterviewSection";
import GenerateHrInterviews from "./HR/GenerateHrInterviews";
import GetHrInterviews from "./HR/GetHrInterviews";
import GenerateTechnicalInterviews from "./Technical/GenerateTechnicalInterviews";
import GetTechnicalInterviews from "./Technical/GetTechnicalInterviews";

const InterviewContainer = () => {
  return (
    <>
      <div className="bg-linear-to-br from-background via-background to-primary p-6">
        <div className="max-w-7xl mx-auto space-y-12">
          <div className="text-center mb-8">
            <h1 className="text-4xl font-bold text-text-primary mb-3">
              Interview Preparation Hub
            </h1>
            <p className="text-lg text-text-secondary max-w-2xl mx-auto">
              Generate AI-powered interviews and review your practice sessions
            </p>
          </div>

          <InterviewSection
            generateInterviews={<GenerateHrInterviews />}
            getInterviews={<GetHrInterviews />}
          />

          <InterviewSection
            generateInterviews={<GenerateTechnicalInterviews />}
            getInterviews={<GetTechnicalInterviews />}
          />
        </div>
      </div>
    </>
  );
}

export default InterviewContainer;