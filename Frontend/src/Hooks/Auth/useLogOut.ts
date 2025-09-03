/**
 * Custom hook for handling user logout functionality.
 * Sends logout request, updates auth context, and navigates to login page.
 * @returns {Object} - hadnleLogOut function
 */
import axios, { AxiosError } from "axios";
import type { SyntheticEvent } from "react";
import { useMutation, useQueryClient } from "react-query";
import { useNavigate } from "react-router-dom";
import { logoutEndPoint } from "../../Utils/endpoints";
import useAuth from "../../Context/useAuth";

const useLogOut = () => {
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const { logout } = useAuth();

  const logOut = async () => {
    await axios
      .post(logoutEndPoint, null, { withCredentials: true })
      .then(() => navigate("/login"))
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        console.error(error?.title);
      });
  };

  const { mutateAsync } = useMutation({
    mutationFn: logOut,
    onSuccess: () => {
      logout();
      queryClient.setQueryData(["user"], null);
      queryClient.invalidateQueries({ queryKey: ["user"] });
    },
  });

  const hadnleLogOut = async (e: SyntheticEvent) => {
    e.preventDefault();
    mutateAsync();
  };

  return { hadnleLogOut };
};

export default useLogOut;
