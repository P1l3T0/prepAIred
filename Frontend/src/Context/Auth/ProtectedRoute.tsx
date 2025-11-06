import { Navigate } from "react-router-dom";
import useAuth from "./useAuth";

interface ProtectedRouteProps {
  children: React.ReactNode;
}

export default function ProtectedRoute({ children }: ProtectedRouteProps) {
  const { isUserLoggedIn } = useAuth();

  return isUserLoggedIn ? children : <Navigate to="/login" replace />;
}
