import { useState, useEffect } from "react";
import { Outlet } from "react-router-dom";
import useRefreshToken from "../../Hooks/Auth/useRefreshToken";
import useAuth from "./useAuth";
import LoaderComponent from "../../Components/Common/LoaderComponent";

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

  return <>{isLoading ? <LoaderComponent /> : <Outlet />}</>;
};

export default PersistLogin;