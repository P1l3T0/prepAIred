import { Button } from "@progress/kendo-react-buttons";
import useLogOut from "../Hooks/Auth/useLogOut";

const LogOutButton = () => {
  const { handleLogOut } = useLogOut();

  return (
    <>
      <Button onClick={handleLogOut}>Logout</Button>
    </>
  );
};

export default LogOutButton;