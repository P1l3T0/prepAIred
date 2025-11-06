interface InterviewContainerProps {
  title: string;
  children: React.ReactNode;
}

const InterviewContainer = ({ title, children }: InterviewContainerProps) => {
  return (
    <div className="bg- rounded-lg shadow-lg p-6 border border-border">
      <h3 className="text-xl font-bold text-text-primary mb-2 text-center">
        {title}
      </h3>
      {children}
    </div>
  );
};

export default InterviewContainer;
