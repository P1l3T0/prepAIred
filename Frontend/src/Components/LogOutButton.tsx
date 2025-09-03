import useLogOut from "../Hooks/Auth/useLogOut";

const LogOutButton = () => {
  const { hadnleLogOut } = useLogOut();

  return (
    <>
      <button onClick={hadnleLogOut}>Logout</button>
    </>
  );
};

export default LogOutButton;