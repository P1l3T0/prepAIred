import axios, { AxiosError } from "axios";
import { useMutation, useQueryClient } from "react-query";
import { deleteInterviewSessionsEndPoint } from "../../Utils/endpoints";

const useDeleteInterviewSessions = () => {
  const queryClient = useQueryClient();

  const deleteInterviewSessions = async () => {
    await axios
      .delete(deleteInterviewSessionsEndPoint, { withCredentials: true })
      .catch((err: AxiosError) => {
        console.error(`Error deleting interview sessions: ${err.message}`);
      });
  };

  const { mutateAsync } = useMutation({
    mutationFn: deleteInterviewSessions,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["interview-sessions"] });
    },
  });

  const handleDelete = async () => {
    await mutateAsync();
  };

  return { handleDelete };
};

export default useDeleteInterviewSessions;