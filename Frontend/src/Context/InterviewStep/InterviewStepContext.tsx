import { createContext, useState, type ReactNode } from "react";
import { type StepProps } from "@progress/kendo-react-layout";
import { eyeIcon, trackChangesIcon } from "@progress/kendo-svg-icons";
import type { InterviewStepContextType } from "../../Utils/interfaces";

export const InterviewStepContext = createContext<InterviewStepContextType | undefined>(undefined);

interface InterviewStepProviderProps {
  children: ReactNode;
}

export const InterviewStepProvider = ({ children }: InterviewStepProviderProps) => {
  const items: StepProps[] = [
    { label: "HR Interview", svgIcon: eyeIcon },
    { label: "Technical Interview", svgIcon: trackChangesIcon },
  ];

  const [value, setValue] = useState<number>(0);

  const handleChangeStep = (step?: number) => {
    if (typeof step === "number") {
      setValue(step);
      return;
    }

    setValue((prev) => prev === 0 ? 1 : 0);
  };

  return (
    <InterviewStepContext.Provider value={{ items, value, handleChangeStep }}>
      {children}
    </InterviewStepContext.Provider>
  );
};