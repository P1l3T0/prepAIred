const baseURL: string | undefined = process.env.REACT_APP_API_URL;

// Controllers
const authController: string | undefined = `${baseURL}/Auth`;
const userController: string | undefined = `${baseURL}/User`;
const interviewController: string | undefined = `${baseURL}/Interview`;

// Auth
export const registerEndPoint = `${authController}/register`;
export const loginEndPoint = `${authController}/login`;
export const logoutEndPoint = `${authController}/logout`;
export const refreshTokenEndPoint = `${authController}/refresh-token`;

// User
export const getCurrentUserEndPoint = `${userController}/get-user`;

//  Interviews
export const generateInterviewsEndPoint = `${interviewController}/generate-interviews`;
export const getInterviewSessionsEndPoint = `${interviewController}/get-interview-sessions`;