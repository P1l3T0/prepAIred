import GenerateHrInterviews from "../Components/Interviews/HR/GenerateHrInterviews";
import GetHrInterviews from "../Components/Interviews/HR/GetHrInterviews";
import GenerateTechnicalInterviews from "../Components/Interviews/Technical/GenerateTechnicalInterviews";
import GetTechnicalInterviews from "../Components/Interviews/Technical/GetTechnicalInterviews";
import useGetUser from "../Hooks/User/useGetUser";

const Home = () => {
  const { data: user, isLoading, isError } = useGetUser();

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>Error</div>;

  return (
    <>
      <div className="bg-background">
        <h1 className="text-text-primary">Welcome, {user?.username}!</h1>
        <p className="text-text-primary">Email: {user?.email}</p>
        <p className="text-text-primary">
          Member since: {user?.dateCreated.toLocaleDateString()}
        </p>
        <p className="text-text-primary">User ID: {user?.id}</p>
      </div>

      <br />

      <GenerateHrInterviews />
      <GetHrInterviews />

      <GenerateTechnicalInterviews />
      <GetTechnicalInterviews />
    </>
  );
};

export default Home;
