import { Button } from "@progress/kendo-react-buttons";
import useLogOut from "../../Hooks/Buttons/useLogOut";

const LogOutButton = () => {
  const { handleLogOut } = useLogOut();

  return (
    <>
      <Button fillMode={"flat"} themeColor={"primary"} onClick={handleLogOut}>
        Logout
      </Button>
    </>
  );
};

export default LogOutButton;