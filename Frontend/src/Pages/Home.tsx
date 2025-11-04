import GenerateHrInterviews from "../Components/Interviews/HR/GenerateHrInterviews";
import GetHrInterviews from "../Components/Interviews/HR/GetHrInterviews";
import GenerateTechnicalInterviews from "../Components/Interviews/Technical/GenerateTechnicalInterviews";
import GetTechnicalInterviews from "../Components/Interviews/Technical/GetTechnicalInterviews";
import LogOutButton from "../Components/Buttons/LogOutButton";
import ThemeButton from "../Components/Buttons/ThemeButton";
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

      <ThemeButton />

      <LogOutButton />

      <br />
      <br />

      <GenerateHrInterviews />
      <GetHrInterviews />

      <GenerateTechnicalInterviews />
      <GetTechnicalInterviews />
    </>
  );
};

export default Home;
