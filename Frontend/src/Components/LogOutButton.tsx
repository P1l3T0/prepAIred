import useLogOut from "../Hooks/Auth/useLogOut";

const LogOutButton = () => {
  const { handleLogOut } = useLogOut();

  return (
    <>
      <button onClick={handleLogOut}>Logout</button>
    </>
  );
};

export default LogOutButton;