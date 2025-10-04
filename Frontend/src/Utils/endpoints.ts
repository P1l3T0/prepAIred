const baseURL: string | undefined = process.env.REACT_APP_API_URL;

// Controllers
const authController: string | undefined = `${baseURL}/Auth`;
const userController: string | undefined = `${baseURL}/User`;
const interviewController: string | undefined = `${baseURL}/Interview`;
const interviewSessionController: string | undefined = `${baseURL}/InterviewSession`;

// Auth
export const registerEndPoint = `${authController}/register`;
export const loginEndPoint = `${authController}/login`;
export const logoutEndPoint = `${authController}/logout`;
export const refreshTokenEndPoint = `${authController}/refresh-token`;

// User
export const getCurrentUserEndPoint = `${userController}/get-user`;

//  Interviews
export const generateHrInterviewsEndPoint = `${interviewController}/generate-hr-interviews`;
export const getLatestHrInterviewsEndPoint = `${interviewController}/get-latest-hr-interviews`;
export const evaluateHrInterviewsEndPoint = `${interviewController}/evaluate-hr-interviews`;
export const generateTechnicalInterviewsEndPoint = `${interviewController}/generate-technical-interviews`;
export const getLatestTechnicalInterviewsEndPoint = `${interviewController}/get-latest-technical-interviews`;
export const evaluateTechnicalInterviewsEndPoint = `${interviewController}/evaluate-technical-interviews`;

// Interview Sessions
export const deleteInterviewSessionsEndPoint = `${interviewSessionController}/delete-interview-sessions`;