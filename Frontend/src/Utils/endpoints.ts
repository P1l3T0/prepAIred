const baseURL: string | undefined = process.env.REACT_APP_API_URL;

// Uploads Folder URL
export const uploadsFolderURL: string = process.env.UPLOADS_FOLDER_URL || "https://localhost:7227/Uploads/";

// Controllers
const authController: string | undefined = `${baseURL}/auth`;
const userController: string | undefined = `${baseURL}/users`;
const profilePictureController: string | undefined = `${baseURL}/profile-pictures`;
const interviewController: string | undefined = `${baseURL}/interviews`;
const interviewSessionController: string | undefined = `${baseURL}/interview-sessions`;

// Auth
export const registerEndPoint = `${authController}/register`;
export const loginEndPoint = `${authController}/login`;
export const logoutEndPoint = `${authController}/logout`;
export const refreshTokenEndPoint = `${authController}/refresh-token`;

// User
export const getCurrentUserEndPoint = `${userController}/me`;
export const updateCurrentUserEndPoint = `${userController}/me`;
export const deleteCurrentUserEndPoint = `${userController}/me`;

// Profile Picture
export const getProfilePictureUrlEndPoint = `${profilePictureController}`;
export const changeProfilePictureEndPoint = `${profilePictureController}`;

//  Interviews
export const generateHrInterviewsEndPoint = `${interviewController}/hr`;
export const getLatestHrInterviewsEndPoint = `${interviewController}/hr/latest`;
export const evaluateHrInterviewsEndPoint = `${interviewController}/hr/evaluations`;
export const generateTechnicalInterviewsEndPoint = `${interviewController}/technical`;
export const getLatestTechnicalInterviewsEndPoint = `${interviewController}/technical/latest`;
export const evaluateTechnicalInterviewsEndPoint = `${interviewController}/technical/evaluations`;

// Interview Sessions
export const getInterviewSessionStatisticsEndPoint = `${interviewSessionController}/statistics`;
export const getRecentInterviewSessionsEndPoint = `${interviewSessionController}/activities`;
export const getInterviewSessionsPerformanceEndPoint = `${interviewSessionController}/performance`;
export const getInterviewSessionsProgrammingLanguageDataEndPoint = `${interviewSessionController}/programming-languages`;
export const getInterviewSessionsPositionDataEndPoint = `${interviewSessionController}/positions`;
export const finishInterviewSessionEndPoint = `${interviewSessionController}/current/finish`;
export const deleteInterviewSessionsEndPoint = `${interviewSessionController}`;