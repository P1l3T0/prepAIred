/**
 * Custom hook for handling user login functionality.
 * Manages user state, handles input changes, and submits login requests.
 * On success, navigates to home and updates auth context.
 * @returns {Object} - handleChange and handleSubmit functions
 */
import axios, { AxiosError } from "axios";
import { useState } from "react";
import { loginEndPoint } from "../../Utils/endpoints";
import { useMutation, useQueryClient } from "react-query";
import { useNavigate } from "react-router-dom";
import type { LoginDto } from "../../Utils/interfaces";
import useAuth from "../../Context/useAuth";

const useLogin = () => {
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const { login } = useAuth();

  const [user, setUser] = useState<LoginDto>({
    email: "",
    password: "",
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setUser({
      ...user,
      [e.target.name]: e.target.value,
    });
  };

  const loginUser = async () => {
    await axios
      .post(loginEndPoint, user, { withCredentials: true })
      .then(() => navigate("/home"))
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        console.error(error?.title);
      });
  };

  const { mutateAsync } = useMutation({
    mutationFn: loginUser,
    onSuccess: () => {
      login();
      queryClient.invalidateQueries({ queryKey: ["user"] });
    },
  });

  const handleSubmit = async () => {
    mutateAsync();
  };

  return { handleChange, handleSubmit };
};

export default useLogin;