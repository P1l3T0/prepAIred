import { Card, CardBody, CardFooter, CardHeader } from "@progress/kendo-react-layout";
import ProfileAvatar from "./ProfileAvatar";
import ProfileUpload from "./ProfileUpload";
import ProfileData from "./ProfileData";
import type { User } from "../../../../Utils/interfaces";
import useUploadProfilePicture from "../../../../Hooks/ProfilePicture/useUploadProfilePicture";
import { Button } from "@progress/kendo-react-buttons";

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
      <CardBody>
        <div className="space-y-4 p-4">
          <div className="flex justify-center mb-6">
            <ProfileAvatar
              username={user?.username!}
              profilePictureUrl={profilePictureUrl}
              onAvatarClick={handleAvatarClick}
            />
          </div>

          <ProfileUpload showUpload={showUpload} onAdd={handleAdd} />

          <ProfileData user={user} />
        </div>
      </CardBody>
      <CardFooter className="flex flex-col md:flex-row justify-center gap-2">
        <Button themeColor={"primary"}>Update Info</Button>
        <Button themeColor={"error"}>Delete User</Button>
      </CardFooter>
    </Card>
  );
};

export default ProfileInfo;
