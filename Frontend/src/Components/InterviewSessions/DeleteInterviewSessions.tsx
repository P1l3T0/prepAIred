import useDeleteInterviewSessions from "../../Hooks/InterviewSessions/useDeleteInterviewSessions";

const DeleteInterviewSessions = () => {
  const { handleDelete } = useDeleteInterviewSessions();

  return (
    <>
      <button onClick={handleDelete}>Delete All Interview Sessions</button>
    </>
  );
};

export default DeleteInterviewSessions;