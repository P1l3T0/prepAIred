import type {
  HRInterviewDTO,
  TechnicalInterviewDTO,
} from "../../../../Utils/interfaces";

interface InterviewFieldsetProps {
  interview: HRInterviewDTO | TechnicalInterviewDTO;
  children: React.ReactNode;
}

const InterviewFieldset = ({ interview, children }: InterviewFieldsetProps) => {
  return (
    <fieldset className="border-[1.5px] border-border rounded-lg mb-8 p-6 bg-card transition-all duration-200 hover:shadow-md hover:border-primary">
      <legend className="font-bold text-lg mb-2 text-primary">
        {interview.interviewType === "HR" ? (
          <>
            <b>Behavioral Context:</b>{" "}
            {(interview as HRInterviewDTO).competencyArea}
          </>
        ) : (
          <>Subject: {(interview as TechnicalInterviewDTO).subject}</>
        )}
        &nbsp; | &nbsp;
        <b>Score:</b> {interview.score || "Not evaluated"}
      </legend>

      <div className="text-text-secondary mb-3 text-base bg-elevated p-2 rounded-md">
        {interview.interviewType === "HR" ? (
          <>
            <b>Behavioral Context:</b>{" "}
            {(interview as HRInterviewDTO).behavioralContext}
          </>
        ) : (
          <>
            <b>Position:</b> {(interview as TechnicalInterviewDTO).position}
          </>
        )}
      </div>

      {children}
    </fieldset>
  );
};

export default InterviewFieldset;
