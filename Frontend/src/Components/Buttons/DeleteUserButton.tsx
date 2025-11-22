import { Button } from "@progress/kendo-react-buttons";
import useDeleteUser from "../../Hooks/User/useDeleteUset";

const DeleteUserButton = () => {
  const handleClick = useDeleteUser();

  return (
    <>
      <Button className="w-full" themeColor={"error"} onClick={handleClick}>Delete</Button>
    </>
  );
};

export default DeleteUserButton;
