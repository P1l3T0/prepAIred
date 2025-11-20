import type { User } from "../../../../Utils/interfaces";
import ProfileSection from "./ProfileSection";

interface ProfileDataProps {
  user: User;
}

const ProfileData = ({ user }: ProfileDataProps) => {
  const date = new Date(user?.dateCreated).toLocaleDateString("en-US", {
    year: "numeric",
    month: "long",
    day: "numeric",
  });

  return (
    <>
      <ProfileSection text="Username" value={user?.username} />
      <ProfileSection text="Email" value={user?.email} />
      <ProfileSection text="Member Since" value={date} />
    </>
  );
};

export default ProfileData;
