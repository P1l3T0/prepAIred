import { Card, CardBody, CardFooter, CardHeader } from "@progress/kendo-react-layout";
import ProfileAvatar from "./ProfileAvatar";
import ProfileUpload from "./ProfileUpload";
import ProfileData from "./ProfileData";
import DeleteUserButton from "../../../Buttons/DeleteUserButton";
import UpdateUserButton from "../../../Buttons/UpdateUserButton";
import useUploadProfilePicture from "../../../../Hooks/Home/ProfilePicture/useUploadProfilePicture";
import useGetUser from "../../../../Hooks/Home/User/useGetUser";
import useGetProfilePictureUrl from "../../../../Hooks/Home/ProfilePicture/useGetProfilePicture";
import LoaderComponent from "../../../Common/LoaderComponent";
import ErrorComponent from "../../../Common/ErrorComponent";

const ProfileInfo = () => {
  const { data: user, isLoading: isUserLoading, isError: isUserError } = useGetUser();
  const { data: profilePictureUrl, isLoading: isProfileLoading, isError: isProfileError } = useGetProfilePictureUrl();
  const { showUpload, handleAvatarClick, handleAdd } = useUploadProfilePicture();

  if (isUserLoading || isProfileLoading) return <LoaderComponent />;
  if (isUserError || isProfileError) return <ErrorComponent />;

  return (
    <Card className="border border-border shadow-md h-full">
      <CardHeader className="border-border">
        <h3 className="text-xl font-medium">Profile Information</h3>
      </CardHeader>
      <CardBody className="flex flex-col">
        <div className="space-y-7 p-4 flex-1 flex flex-col">
          <div className="flex items-center gap-5 ">
            <ProfileAvatar
              username={user?.username!}
              profilePictureUrl={profilePictureUrl!}
              onAvatarClick={handleAvatarClick}
            />

            <ProfileUpload showUpload={showUpload} onAdd={handleAdd} />
          </div>

          <div className="flex flex-col mt-auto">
            <ProfileData user={user!} />
          </div>
        </div>
      </CardBody>
      <CardFooter className="flex flex-col lg:flex-row justify-center gap-2">
        <UpdateUserButton />
        <DeleteUserButton />
      </CardFooter>
    </Card>
  );
};

export default ProfileInfo;