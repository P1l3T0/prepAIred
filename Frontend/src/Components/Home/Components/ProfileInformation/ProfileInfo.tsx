import { Card, CardBody, CardFooter, CardHeader } from "@progress/kendo-react-layout";
import ProfileAvatar from "./ProfileAvatar";
import ProfileUpload from "./ProfileUpload";
import ProfileData from "./ProfileData";
import type { User } from "../../../../Utils/interfaces";
import DeleteUserButton from "../../../Buttons/DeleteUserButton";
import UpdateUserButton from "../../../Buttons/UpdateUserButton";
import useUploadProfilePicture from "../../../../Hooks/Home/ProfilePicture/useUploadProfilePicture";

interface ProfileInfoProps {
  profilePictureUrl: string;
  user: User;
}

const ProfileInfo = ({ user, profilePictureUrl }: ProfileInfoProps) => {
  const { showUpload, handleAvatarClick, handleAdd } = useUploadProfilePicture();

  return (
    <Card className="border border-border shadow-sm shadow-primary h-full">
      <CardHeader className="border-border">
        <h3 className="text-xl font-medium">Profile Information</h3>
      </CardHeader>
      <CardBody className="flex flex-col">
        <div className="space-y-7 p-4 flex-1 flex flex-col">
          <div className="flex justify-center">
            <ProfileAvatar
              username={user?.username!}
              profilePictureUrl={profilePictureUrl}
              onAvatarClick={handleAvatarClick}
            />
          </div>

          <ProfileUpload showUpload={showUpload} onAdd={handleAdd} />

          <div className="flex flex-col mt-auto">
            <ProfileData user={user} />
          </div>
        </div>
      </CardBody>
      <CardFooter className="flex flex-col md:flex-row justify-center gap-2">
        <UpdateUserButton />
        <DeleteUserButton />
      </CardFooter>
    </Card>
  );
};

export default ProfileInfo;
