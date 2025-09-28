/**
 * Custom hook for handling user registration functionality.
 * Manages user state, handles input changes, and submits registration requests.
 * On success, navigates to home and updates auth context.
 * @returns {Object} - handleChange and handleSubmit functions
 */
import axios, { AxiosError } from "axios";
import { useState } from "react";
import { useMutation, useQueryClient } from "react-query";
import { useNavigate } from "react-router-dom";
import { registerEndPoint } from "../../Utils/endpoints";
import type { RegisterDto } from "../../Utils/interfaces";
import useAuth from "../../Context/useAuth";

const useRegister = () => {
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const { login } = useAuth();

  const [user, setUser] = useState<RegisterDto>({
    email: "",
    username: "",
    password: "",
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setUser({
      ...user,
      [e.target.name]: e.target.value,
    });
  };

  const registerUser = async () => {
    await axios
      .post(registerEndPoint, user, { withCredentials: true })
      .then(() => navigate("/home"))
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        console.error(error?.title);
      });
  };

  const { mutateAsync } = useMutation({
    mutationFn: registerUser,
    onSuccess: () => {
      login();
      queryClient.invalidateQueries({ queryKey: ["user"] });
    }
  });

  const handleSubmit = async () => {
    mutateAsync();
  };

  return { handleChange, handleSubmit };
};

export default useRegister;