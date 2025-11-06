import GenerateHrInterviews from "../Components/Interviews/HR/GenerateHrInterviews";
import GetHrInterviews from "../Components/Interviews/HR/GetHrInterviews";
import InterviewSection from "../Components/Interviews/IntterviewSection";
import GenerateTechnicalInterviews from "../Components/Interviews/Technical/GenerateTechnicalInterviews";
import GetTechnicalInterviews from "../Components/Interviews/Technical/GetTechnicalInterviews";

const Interviews = () => {
  return (
    <div className="bg-background p-6">
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
          interviewType="HR-Interview"
          generateInterviews={<GenerateHrInterviews />}
          getInterviews={<GetHrInterviews />}
        />

        <InterviewSection
          interviewType="Technical-Interview"
          generateInterviews={<GenerateTechnicalInterviews />}
          getInterviews={<GetTechnicalInterviews />}
        />
      </div>
    </div>
  );
};

export default Interviews;
