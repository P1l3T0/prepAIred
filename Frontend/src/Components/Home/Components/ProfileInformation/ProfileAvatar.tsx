import { Avatar } from "@progress/kendo-react-layout";

interface ProfileAvatarProps {
  username: string;
  onAvatarClick: () => void;
}

const profileImage = "https://demos.telerik.com/kendo-react-ui/assets/dropdowns/contacts/RICSU.jpg";

const ProfileAvatar = ({ username, onAvatarClick }: ProfileAvatarProps) => {
  return (
    <div className="relative group cursor-pointer" onClick={onAvatarClick}>
      <Avatar
        size={"large"}
        className={`ring-4 ring-primary/20 transition-all hover:ring-primary/40 hover:scale-105`}
      >
        {profileImage ? (
          <img src={profileImage} alt="Profile Avatar" />
        ) : (
          username.charAt(0).toUpperCase() || "U"
        )}
      </Avatar>
    </div>
  );
};

export default ProfileAvatar;
