import { Button } from "@progress/kendo-react-buttons";
import useDeleteUser from "../../Hooks/User/useDeleteUset";

const DeleteUserButton = () => {
  const handleClick = useDeleteUser();

  return (
    <>
      <Button themeColor={"error"} onClick={handleClick}>Delete User</Button>
    </>
  );
};

export default DeleteUserButton;
