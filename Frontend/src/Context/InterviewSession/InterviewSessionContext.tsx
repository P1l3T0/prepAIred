import React, { createContext, useState, useEffect } from 'react';
import type { ReactNode } from 'react';
import { useQuery } from 'react-query';
import axios from 'axios';
import { getLatestTechnicalInterviewsEndPoint } from '../../Utils/endpoints';
import type { InterviewDTO } from '../../Utils/interfaces';

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

  const { data: technicalInterviews } = useQuery(
    ["technical-interviews"],
    async () => {
      const response = await axios.get(getLatestTechnicalInterviewsEndPoint, {
        withCredentials: true,
      });
      return response.data;
    }
  );

  useEffect(() => {
    const technicalCompleted =
      technicalInterviews &&
      technicalInterviews.length > 0 &&
      technicalInterviews.every(
        (interview: InterviewDTO) => interview.isAnswered
      );

    setIsReadyToFinish(technicalCompleted);

    technicalCompleted ? setShowFinishButton(true) : setShowFinishButton(false);
  }, [technicalInterviews]);

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