import { useMutation, useQueryClient } from "react-query";
import axios, { AxiosError } from "axios";
import { deleteCurrentUserEndPoint } from "../../Utils/endpoints";
import useAuth from "../../Context/Auth/useAuth";
import { useNavigate } from "react-router-dom";

const useDeleteUser = () => {
  const queryClient = useQueryClient();
  const navigate = useNavigate();
  const { logout } = useAuth();

  const deleteUser = async () => {
    await axios
      .delete(deleteCurrentUserEndPoint, { withCredentials: true })
      .catch((err: AxiosError) => {
        const error = err.response?.data as { title?: string };
        console.error(error?.title);
      });
  };

  const { mutateAsync } = useMutation({
    mutationFn: deleteUser,
    onSuccess: () => {
      logout();
      navigate("/");
      queryClient.invalidateQueries({ queryKey: ["user"] });
    },
  });

  const handleClick = async () => {
    await mutateAsync();
  };

  return handleClick;
};

export default useDeleteUser;
