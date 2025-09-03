import { useState, useEffect } from "react";
import { Outlet } from "react-router-dom";
import useRefreshToken from "../Hooks/Auth/useRefreshToken";
import useAuth from "./useAuth";

const PersistLogin = () => {
  const [isLoading, setIsLoading] = useState(true);
  const refresh = useRefreshToken();
  const { auth, setAuth } = useAuth();

  useEffect(() => {
    const verifyRefreshToken = async () => {
      try {
        const newAccessToken = await refresh();
        setAuth((prev) => ({ ...prev, accessToken: newAccessToken }));
      } catch (error) {
      } finally {
        setIsLoading(false);
      }
    };

    !auth?.accessToken ? verifyRefreshToken() : setIsLoading(false);
  }, []);

  return <>{isLoading ? "" : <Outlet />}</>;
};

export default PersistLogin;