import { createContext, useState, type ReactNode } from "react";

interface InterviewGenerateButtonContextType {
  disableHrInterviewButton: boolean;
  disableTechnicalInterviewButton: boolean;
  setDisableHrInterviewButton: (disable: boolean) => void;
  setDisableTechnicalInterviewButton: (disable: boolean) => void;
}

interface InterviewGenerateButtonProviderProps {
  children: ReactNode;
}

export const InterviewGenerateButtonContext = createContext<InterviewGenerateButtonContextType | undefined>(undefined);

const InterviewGenerateButtonContextProvider = ({ children }: InterviewGenerateButtonProviderProps) => {
  const [disableHrInterviewButton, setDisableHrInterviewButton] = useState<boolean>(() => {
    const saved = localStorage.getItem('disableHrInterviewButton');
    return saved ? JSON.parse(saved) : false;
  });
  
  const [disableTechnicalInterviewButton, setDisableTechnicalInterviewButton] = useState<boolean>(() => {
    const saved = localStorage.getItem('disableTechnicalInterviewButton');
    return saved ? JSON.parse(saved) : false;
  });

  const updateDisableHrInterviewButton = (disable: boolean) => {
    setDisableHrInterviewButton(disable);
    localStorage.setItem('disableHrInterviewButton', JSON.stringify(disable));
  };

  const updateDisableTechnicalInterviewButton = (disable: boolean) => {
    setDisableTechnicalInterviewButton(disable);
    localStorage.setItem('disableTechnicalInterviewButton', JSON.stringify(disable));
  };

  const contextValue: InterviewGenerateButtonContextType = {
    disableHrInterviewButton,
    setDisableHrInterviewButton: updateDisableHrInterviewButton,
    disableTechnicalInterviewButton,
    setDisableTechnicalInterviewButton: updateDisableTechnicalInterviewButton
  };

  return (
    <InterviewGenerateButtonContext.Provider value={contextValue}>
      {children}
    </InterviewGenerateButtonContext.Provider>
  )
}

export default InterviewGenerateButtonContextProvider;