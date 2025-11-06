import useLogOut from "../../Hooks/Buttons/useLogOut";
import LinkButton from "./LinkButton";

const LogOutButton = () => {
  const { handleLogOut } = useLogOut();

  return (
    <>
      <LinkButton to="/" label="Logout" onClick={handleLogOut} />
    </>
  );
};

export default LogOutButton;