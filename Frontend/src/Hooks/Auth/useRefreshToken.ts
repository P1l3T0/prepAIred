/**
 * Custom hook for refreshing JWT tokens at a set interval. Handles token refresh logic and updates auth context.
 * @returns {Function} - refresh function to manually trigger token refresh
 */
import axios from "axios";
import { useRef, useState, useEffect } from "react";
import { refreshTokenEndPoint } from "../../Utils/endpoints";
import useAuth from "../../Context/Auth/useAuth";

const DEFAULT_REFRESH_INTERVAL = 5 * 60;

const useRefreshToken = () => {
  const tokenRefreshTimeout = useRef<NodeJS.Timeout | null>(null);
  const { setAuth, isUserLoggedIn } = useAuth();

  /**
   * Helper function to get a cookie value by name.
   * @param {string} name - The name of the cookie to retrieve
   * @returns {string | undefined} - The cookie value if found, otherwise undefined
   */
  function getCookie(name: string): string | undefined {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);

    if (parts.length === 2) return parts.pop()?.split(";").shift();
    return undefined;
  }

  const refreshTokenCookie = getCookie("RefreshToken");
  const [refreshToken, setRefreshToken] = useState({ refreshToken: refreshTokenCookie });

  /**
   * Sends refresh token request to the backend API. Updates auth context and schedules next refresh.
   * @returns {Promise<string|undefined>} - New access token or undefined
   */
  const refresh = async () => {
    if (!isUserLoggedIn) return;

    const response = await axios.post(`${refreshTokenEndPoint}`, refreshToken, { withCredentials: true });
    const { username, newAccessToken, newRefreshToken, expiresIn } = response.data;

    setAuth((prev) => ({
      ...prev,
      username,
      accessToken: newAccessToken,
    }));

    setRefreshToken({ refreshToken: newRefreshToken });
    setTokenRefreshTimer(expiresIn ? expiresIn - 10 : DEFAULT_REFRESH_INTERVAL);

    return newAccessToken;
  };

  /**
   * Sets a timer to refresh the token before expiration.
   * @param {number} expiresIn - Time in seconds until token expires
   */
  const setTokenRefreshTimer = (expiresIn: number) => {
    if (tokenRefreshTimeout.current) {
      clearTimeout(tokenRefreshTimeout.current);
    }
    tokenRefreshTimeout.current = setTimeout(() => {
      refresh();
    }, expiresIn * 1000);
  };

  useEffect(() => {
    setTokenRefreshTimer(DEFAULT_REFRESH_INTERVAL);
    return () => {
      if (tokenRefreshTimeout.current) {
        clearTimeout(tokenRefreshTimeout.current);
      }
    };
  }, []);

  return refresh;
};

export default useRefreshToken;