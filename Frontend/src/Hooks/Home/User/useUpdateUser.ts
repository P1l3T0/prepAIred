import { useState } from "react";
import axios, { AxiosError } from "axios";
import { useMutation, useQueryClient } from "react-query";
import type { TextBoxChangeEvent } from "@progress/kendo-react-inputs";
import { updateCurrentUserEndPoint } from "../../../Utils/endpoints";

interface UpdateUserDTO {
  username: string;
  email: string;
  password: string;
}

const useUpdateUser = () => {
  const queryClient = useQueryClient();
  const [visible, setVisible] = useState<boolean>(false);

  const [updatedUser, setUpdatedUser] = useState<UpdateUserDTO>({
    username: "",
    email: "",
    password: "",
  });

  const toggleDialog = () => {
    setVisible((prev) => !prev);
  };

  const handleChange = (e: TextBoxChangeEvent) => {
    const name: string = e.target.name || "";
    const value = (e.target.value as string).trim();

    setUpdatedUser({
      ...updatedUser,
      [name]: value,
    });
  };

  const updateUser = async () => {
    await axios
      .put<UpdateUserDTO>(updateCurrentUserEndPoint, updatedUser, { withCredentials: true })
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        console.error(error?.title);
      });
  };

  const { mutateAsync } = useMutation({
    mutationFn: updateUser,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["user"] });
    },
  });

  const handleUpdate = async () => {
    await mutateAsync();
  };

  return {
    visible,
    toggleDialog,
    handleChange,
    handleUpdate,
  };
};

export default useUpdateUser;
