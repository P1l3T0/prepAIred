// Auth

export type AuthProviderProps = {
  children: React.ReactNode;
};

export type AuthState = {
  username?: string;
  accessToken?: string;
  refreshToken?: string;
};

export type AuthContextType = {
  auth: AuthState;
  setAuth: React.Dispatch<React.SetStateAction<AuthState>>;
  isUserLoggedIn: boolean;
  login: () => void;
  logout: () => void;
};

// Register/Login

export type RegisterDto = {
  email: string;
  username: string;
  password: string;
};

export type LoginDto = {
  email: string;
  password: string;
};

// User

export type User = {
  id: string;
  username: string;
  email: string;
  dateCreated: Date;
};
