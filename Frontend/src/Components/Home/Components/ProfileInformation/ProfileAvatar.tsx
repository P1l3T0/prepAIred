import { Avatar } from "@progress/kendo-react-layout";

interface ProfileAvatarProps {
  username: string;
  profilePictureUrl: string;
  onAvatarClick: () => void;
}

const ProfileAvatar = ({ username, profilePictureUrl, onAvatarClick }: ProfileAvatarProps) => {
  return (
    <div className="relative group cursor-pointer" onClick={onAvatarClick}>
      <Avatar
        size={"large"}
        className={`ring-4 ring-primary/20 transition-all hover:ring-primary/40 hover:scale-105`}
      >
        {profilePictureUrl ? (
          <img src={profilePictureUrl} alt="Profile Avatar" />
        ) : (
          username.charAt(0).toUpperCase() || "U"
        )}
      </Avatar>
    </div>
  );
};

export default ProfileAvatar;
