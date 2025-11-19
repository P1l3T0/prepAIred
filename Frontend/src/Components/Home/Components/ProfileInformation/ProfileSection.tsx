import { Label } from '@progress/kendo-react-labels';

interface ProfileSectionProps {
  text: string;
  value: string;
}

const ProfileSection = ({ text, value }: ProfileSectionProps) => {
  return (
    <div>
      <Label className="text-sm font-medium text-text-secondary">{text}</Label>
      <p className="text-text-primary font-medium">{value}</p>
    </div>
  );
};

export default ProfileSection;