const baseURL: string | undefined = process.env.REACT_APP_API_URL;

// Uploads Folder URL
export const uploadsFolderURL: string = process.env.UPLOADS_FOLDER_URL || "https://localhost:7227/Uploads/";

// Controllers
const authController: string | undefined = `${baseURL}/Auth`;
const userController: string | undefined = `${baseURL}/User`;
const profilePictureController: string | undefined = `${baseURL}/ProfilePicture`;
const interviewController: string | undefined = `${baseURL}/Interview`;
const interviewSessionController: string | undefined = `${baseURL}/InterviewSession`;

// Auth
export const registerEndPoint = `${authController}/register`;
export const loginEndPoint = `${authController}/login`;
export const logoutEndPoint = `${authController}/logout`;
export const refreshTokenEndPoint = `${authController}/refresh-token`;

// User
export const getCurrentUserEndPoint = `${userController}/get-current-user`;
export const updateCurrentUserEndPoint = `${userController}/update-current-user`;
export const deleteCurrentUserEndPoint = `${userController}/delete-current-user`;

// Profile Picture
export const getProfilePictureUrlEndPoint = `${profilePictureController}/get-profile-picture-url`;
export const changeProfilePictureEndPoint = `${profilePictureController}/change-profile-picture`;

//  Interviews
export const generateHrInterviewsEndPoint = `${interviewController}/generate-hr-interviews`;
export const getLatestHrInterviewsEndPoint = `${interviewController}/get-latest-hr-interviews`;
export const evaluateHrInterviewsEndPoint = `${interviewController}/evaluate-hr-interviews`;
export const generateTechnicalInterviewsEndPoint = `${interviewController}/generate-technical-interviews`;
export const getLatestTechnicalInterviewsEndPoint = `${interviewController}/get-latest-technical-interviews`;
export const evaluateTechnicalInterviewsEndPoint = `${interviewController}/evaluate-technical-interviews`;

// Interview Sessions
export const deleteInterviewSessionsEndPoint = `${interviewSessionController}/delete-interview-sessions`;