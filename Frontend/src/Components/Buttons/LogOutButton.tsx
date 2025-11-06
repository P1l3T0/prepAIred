import { Button } from "@progress/kendo-react-buttons";
import useLogOut from "../../Hooks/Buttons/useLogOut";

const LogOutButton = () => {
  const { handleLogOut } = useLogOut();

  return (
    <>
      <Button onClick={handleLogOut}>Logout</Button>
    </>
  );
};

export default LogOutButton;