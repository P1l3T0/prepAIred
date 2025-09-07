export type AIRequestDTO = {
  aiAgent: string;
  field: string;
  level: string;
  numberOfQuestions: number;
};

export type InterviewSessionDTO = {
  id: number;
  dateCreated: Date;
  userID: number;
  topic: string;
  isCompleted: boolean;
  aiAgent: string;
  score: string;
  interviews: InterviewDTO[];
};

export type InterviewDTO = {
  id: number;
  question: string;
  dateCreated: Date;
  isAnswered: boolean;
  userID: number;
  interviewSessionID: number;
  answers: string[];
};