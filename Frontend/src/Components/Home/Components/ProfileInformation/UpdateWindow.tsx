import { Button } from "@progress/kendo-react-buttons";
import { Window } from "@progress/kendo-react-dialogs";
import { TextBox, type TextBoxChangeEvent } from "@progress/kendo-react-inputs";
import { Label } from "@progress/kendo-react-labels";

interface UpdateWindowProps {
  toggleDialog: () => void;
  handleUpdate: () => void;
  handleChange: (e: TextBoxChangeEvent) => void;
}

const UpdateWindow = ({ toggleDialog, handleChange, handleUpdate }: UpdateWindowProps) => {
  return (
    <>
      <Window title="Update Profile" onClose={toggleDialog} initialHeight={370}>
        <form className="space-y-3">
          <div className="space-y-2">
            <div>
              <Label className="text-sm font-medium text-text-secondary">Username</Label>
              <TextBox id="name" type="text" name="username" autoComplete="off" onChange={handleChange} />
            </div>
            <div>
              <Label className="text-sm font-medium text-text-secondary">Email</Label>
              <TextBox id="email" type="email" name="email" autoComplete="off" onChange={handleChange} />
            </div>
            <div>
              <Label className="text-sm font-medium text-text-secondary">Password (not required)</Label>
              <TextBox id="password" type="password" name="password" autoComplete="off" onChange={handleChange} />
            </div>
          </div>
          <div className="flex justify-center gap-2 pt-5">
            <Button type="button" themeColor="primary" className="w-full" onClick={handleUpdate}>Update</Button>
            <Button type="button" themeColor="error" className="w-full" onClick={toggleDialog}>Cancel</Button>
          </div>
        </form>
      </Window>
    </>
  )
}

export default UpdateWindow;