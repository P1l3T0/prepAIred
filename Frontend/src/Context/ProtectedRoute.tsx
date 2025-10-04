import { Navigate } from "react-router-dom";
import useAuth from "./useAuth";

export default function ProtectedRoute({ children } : { children: React.ReactNode }) {
  const { isUserLoggedIn } = useAuth();

  if (!isUserLoggedIn) {
    return <Navigate to="/authenticate" replace />;
  }

  return children;
}
