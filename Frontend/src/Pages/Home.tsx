import AiInterviews from "../Components/InterviewSessions/GenerateInterviewSession";
import GetAiInterviews from "../Components/InterviewSessions/GetInterviewSession";
import LogOutButton from "../Components/LogOutButton";
import useGetUser from "../Hooks/User/useGetUser";

const Home = () => {
  const { data: user, isLoading, isError } = useGetUser();

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>Error</div>;

  return (
    <>
      <h1>Welcome, {user?.username}!</h1>
      <p>Email: {user?.email}</p>
      <p>Member since: {user?.dateCreated.toLocaleDateString()}</p>
      <p>User ID: {user?.id}</p>

      <LogOutButton />

      <br />
      <br />
      <AiInterviews />
      <GetAiInterviews />
    </>
  );
};

export default Home;
