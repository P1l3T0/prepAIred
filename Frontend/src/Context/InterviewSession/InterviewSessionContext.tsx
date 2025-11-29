import React, { createContext, useState, useEffect } from 'react';
import type { ReactNode } from 'react';
import { useQuery } from 'react-query';
import axios from 'axios';
import { getLatestTechnicalInterviewsEndPoint, getLatestHrInterviewsEndPoint } from '../../Utils/endpoints';
import type { InterviewDTO } from '../../Utils/interfaces';
import useInterviewGenerateButton from '../InterviewGenerateButton/useInterviewGenerateButton';

interface InterviewSessionContextType {
  showFinishButton: boolean;
  isReadyToFinish: boolean;
  setShowFinishButton: (show: boolean) => void;
}

interface InterviewSessionProviderProps {
  children: ReactNode;
}

export const InterviewSessionContext = createContext<InterviewSessionContextType | undefined>(undefined);

const InterviewSessionContextProvider: React.FC<InterviewSessionProviderProps> = ({ children }) => {
  const [showFinishButton, setShowFinishButton] = useState<boolean>(false);
  const [isReadyToFinish, setIsReadyToFinish] = useState<boolean>(false);

  const { setDisableTechnicalInterviewButton, setDisableHrInterviewButton } = useInterviewGenerateButton();

  const { data: technicalInterviews } = useQuery(["technical-interviews"],
    async () => {
      const response = await axios.get(getLatestTechnicalInterviewsEndPoint, { withCredentials: true });
      return response.data;
    }
  );

  const { data: hrInterviews } = useQuery(["hr-interviews"],
    async () => {
      const response = await axios.get(getLatestHrInterviewsEndPoint, { withCredentials: true });
      return response.data;
    }
  );

  useEffect(() => {
    const hasHrInterviews: boolean = hrInterviews?.length > 0;
    const hasTechnicalInterviews: boolean = technicalInterviews?.length > 0;

    const allTechnicalAnswered: boolean = technicalInterviews?.every((interview: InterviewDTO) => interview.isAnswered);
    const technicalCompleted: boolean = hasTechnicalInterviews && allTechnicalAnswered;

    setDisableHrInterviewButton(hasHrInterviews);
    setDisableTechnicalInterviewButton(hasTechnicalInterviews);
    setIsReadyToFinish(technicalCompleted);

    technicalCompleted ? setShowFinishButton(true) : setShowFinishButton(false);
  }, [hrInterviews, technicalInterviews]);

  const contextValue: InterviewSessionContextType = {
    showFinishButton,
    setShowFinishButton,
    isReadyToFinish,
  };

  return (
    <InterviewSessionContext.Provider value={contextValue}>
      {children}
    </InterviewSessionContext.Provider>
  );
};

export default InterviewSessionContextProvider;