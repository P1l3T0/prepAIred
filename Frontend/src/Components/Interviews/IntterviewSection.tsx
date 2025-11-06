interface InterviewSectionProps {
  interviewType: "HR-Interview" | "Technical-Interview";
  generateInterviews: React.ReactNode;
  getInterviews: React.ReactNode;
}

const InterviewSection = ({
  interviewType,
  generateInterviews,
  getInterviews,
}: InterviewSectionProps) => {
  return (
    <section className="space-y-6">
      <div className="flex items-center gap-3 mb-6 text-center justify-center">
        <h2 className="text-3xl font-bold text-text-primary">
          {interviewType === "HR-Interview"
            ? "HR Interviews"
            : "Technical Interviews"}
        </h2>
      </div>
      <div className="space-y-6 flex flex-col items-center">  
        <div className="max-w-5xl w-full">{generateInterviews}</div>
        <div className="max-w-5xl w-full">{getInterviews}</div>
      </div>
    </section>
  );
};

export default InterviewSection;
