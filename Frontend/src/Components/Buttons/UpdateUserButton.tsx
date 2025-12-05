import { Button } from "@progress/kendo-react-buttons";
import useUpdateUser from "../../Hooks/Home/User/useUpdateUser";
import UpdateWindow from "../Home/Components/ProfileInformation/UpdateWindow";

const UpdateUserButton = () => {
  const { visible, toggleDialog, handleChange, handleUpdate } = useUpdateUser();

  return (
    <>
      <Button className="w-full" themeColor={"primary"} onClick={toggleDialog}>Update</Button>

      {visible && (
        <UpdateWindow
          toggleDialog={toggleDialog}
          handleChange={handleChange}
          handleUpdate={handleUpdate}
        />
      )}
    </>
  );
};

export default UpdateUserButton;
