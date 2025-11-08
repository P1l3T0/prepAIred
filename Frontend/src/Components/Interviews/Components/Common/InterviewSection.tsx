interface InterviewSectionProps {
  generateInterviews: React.ReactNode;
  getInterviews: React.ReactNode;
}

const InterviewSection = ({
  generateInterviews,
  getInterviews,
}: InterviewSectionProps) => {
  return (
    <section className="space-y-6">
      <div className="space-y-6 flex flex-col items-center">
        <div className="max-w-5xl w-full">{generateInterviews}</div>
        <div className="max-w-5xl w-full">{getInterviews}</div>
      </div>
    </section>
  );
};

export default InterviewSection;
