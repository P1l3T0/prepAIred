import { createContext, useState } from "react";
import type { AuthContextType, AuthState } from "../../Utils/interfaces";

interface AuthProviderProps {
  children: React.ReactNode;
}

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
