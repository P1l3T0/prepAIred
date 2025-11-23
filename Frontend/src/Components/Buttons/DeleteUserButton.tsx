import { Button } from "@progress/kendo-react-buttons";
import useDeleteUser from "../../Hooks/User/useDeleteUser";

const DeleteUserButton = () => {
  const handleClick = useDeleteUser();

  return (
    <>
      <Button className="w-full" themeColor={"error"} onClick={handleClick}>Delete</Button>
    </>
  );
};

export default DeleteUserButton;
