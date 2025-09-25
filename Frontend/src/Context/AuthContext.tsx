import { createContext, useState } from "react";
import type { AuthContextType, AuthProviderProps, AuthState } from "../Utils/interfaces";

export const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [auth, setAuth] = useState<AuthState>({});
  const [isUserLoggedIn, setIsUserLoggedIn] = useState(document.cookie.includes("RefreshToken"));
  const login = () => setIsUserLoggedIn(true);
  const logout = () => setIsUserLoggedIn(false);

  return (
    <AuthContext.Provider value={{ auth, setAuth, isUserLoggedIn, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
