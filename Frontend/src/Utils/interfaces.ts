// #region Auth

export interface AuthProviderProps {
  children: React.ReactNode;
}

export interface AuthState {
  username?: string;
  accessToken?: string;
  refreshToken?: string;
}

export interface AuthContextType {
  auth: AuthState;
  isUserLoggedIn: boolean;
  setAuth: React.Dispatch<React.SetStateAction<AuthState>>;
  login: () => void;
  logout: () => void;
}

// Register/Login DTOs

export interface RegisterDto {
  email: string;
  username: string;
  password: string;
}

export interface LoginDto {
  email: string;
  password: string;
}

// User

export interface User {
  id: string;
  username: string;
  email: string;
  dateCreated: Date;
}

//#endregion

//#region Request DTOs

interface BaseRequestDTO {
  aiAgent: string;
  numberOfQuestions: number;
  hasPriorExperience: boolean;
}

export interface TechnicalRequestDTO extends BaseRequestDTO {
  programmingLanguage: string;
  subject: string[];
  difficultyLevel: string;
  position: string;
}

export interface HrRequestDTO extends BaseRequestDTO {
  softSkillFocus: string[];
  contextScenario: string[];
}

export interface EvaluateRequestDTO {
  question: string;
  answer: string | string[];
}

export interface UseEvaluateInterviewsProps {
  interviewType: "HR-Interview" | "Technical-Interview";
  singleChoiceAnswers: Answer[];
  multipleChoiceAnswers: MultipleChoiceAnswer[];
  openEndedAnswers: Answer[];
}

//#endregion

//#region Response DTOs

interface BaseDTO {
  id: number;
  dateCreated: Date;
}

// InterviewDTO
export interface InterviewDTO extends BaseDTO {
  question: string;
  questionType: string;
  isAnswered: boolean;
  userID: number;
  interviewSessionID: number;
  interviewType: string;
  answers: string[];
  feedback: string;
  score: number;
  selectedAnswer: string;
}

export interface TechnicalInterviewDTO extends InterviewDTO {
  programmingLanguage: string;
  difficultyLevel: string;
  subject: string;
  position: string;
}

export interface HRInterviewDTO extends InterviewDTO {
  softSkillFocus: string;
  competencyArea: string;
  behavioralContext: string;
}

export interface InterviewSessionDTO extends BaseDTO {
  userID: number;
  subject: string;
  isCompleted: boolean;
  aiAgent: string;
  score: string;
  interviews: InterviewDTO[];
}

//#endregion

//#region Answers

export interface AnswersProps {
  interview: InterviewDTO;
  interviewType: string;
  interviewIndex: number;
  onChange: (value: string) => void;
}

export interface Answer {
  question: string;
  answer: string;
}

export interface MultipleChoiceAnswer {
  question: string;
  answers: string[];
}

//#endregion

export interface ChangeStepButtonProps {
  handleChangeStep: () => void;
}