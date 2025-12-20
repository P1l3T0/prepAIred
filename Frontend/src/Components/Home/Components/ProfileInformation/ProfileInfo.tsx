import { Card, CardBody, CardFooter, CardHeader } from "@progress/kendo-react-layout";
import ProfileAvatar from "./ProfileAvatar";
import ProfileUpload from "./ProfileUpload";
import ProfileData from "./ProfileData";
import DeleteUserButton from "../../../Buttons/DeleteUserButton";
import UpdateUserButton from "../../../Buttons/UpdateUserButton";
import useUploadProfilePicture from "../../../../Hooks/Home/ProfilePicture/useUploadProfilePicture";
import useGetUser from "../../../../Hooks/Home/User/useGetUser";
import useGetProfilePictureUrl from "../../../../Hooks/Home/ProfilePicture/useGetProfilePicture";
import { Loader } from "@progress/kendo-react-indicators";

const ProfileInfo = () => {
  const { data: user, isLoading: isUserLoading, isError: isUserError } = useGetUser();
  const { data: profilePictureUrl, isLoading: isProfileLoading, isError: isProfileError } = useGetProfilePictureUrl();
  const { showUpload, handleAvatarClick, handleAdd } = useUploadProfilePicture();

  if (isUserLoading || isProfileLoading) {
    return (
      <div className="min-h-[calc(100vh-4.05rem)] sm:min-h-[calc(100vh-4.55rem)] bg-background flex items-center justify-center">
        <div className="flex flex-col items-center space-y-4">
          <Loader size="large" />
          <span className="text-text-secondary">Loading your profile...</span>
        </div>
      </div>
    );
  }

  if (isUserError || isProfileError) {
    return (
      <div className="min-h-[calc(100vh-4.05rem)] sm:min-h-[calc(100vh-4.55rem)] bg-background flex items-center justify-center">
        <Card className="shadow-lg">
          <CardBody>
            <div className="text-center p-8">
              <h2 className="text-xl text-text-primary font-semibold mb-2">
                Unable to load profile
              </h2>
              <p className="text-text-secondary">
                Please try refreshing the page
              </p>
            </div>
          </CardBody>
        </Card>
      </div>
    );
  }

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
