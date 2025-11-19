import { Label } from "@progress/kendo-react-labels";
import { ProgressBar } from "@progress/kendo-react-progressbars";
import type { User } from "../../../../Utils/interfaces";
import ProfileSection from "./ProfileSection";

interface ProfileDataProps {
  completionRate: number;
  user: User;
}

const ProfileData = ({ completionRate, user }: ProfileDataProps) => {
  const date = new Date(user?.dateCreated).toLocaleDateString("en-US", {
    year: "numeric",
    month: "long",
    day: "numeric",
  });

  return (
    <>
      <div className="space-y-3">
        <ProfileSection text="Username" value={user?.username} />
        <ProfileSection text="Email" value={user?.email} />
        <ProfileSection text="Member Since" value={date} />
      </div>

      <div>
        <Label className="text-sm font-medium text-text-secondary">
          Completion Rate
        </Label>
        <ProgressBar
          progressStyle={{ backgroundColor: "#3b82f6" }}
          value={completionRate! || 0}
          label={(props) => {
            return <>{Math.round(props.value as number)}%</>;
          }}
        />
      </div>
    </>
  );
};

export default ProfileData;
