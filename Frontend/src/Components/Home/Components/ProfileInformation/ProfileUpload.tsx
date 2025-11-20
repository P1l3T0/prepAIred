import { Upload, type UploadFileRestrictions, type UploadOnAddEvent } from "@progress/kendo-react-upload";

interface ProfileUploadProps {
  showUpload: boolean;
  onAdd: (e: UploadOnAddEvent) => void;
}

const restrictions: UploadFileRestrictions = {
  allowedExtensions: [".jpg", ".jpeg", ".png", ".gif"],
  maxFileSize: 2 * 1024 * 1024
};

const ProfileUpload = ({
  showUpload,
  onAdd,
}: ProfileUploadProps) => {

  return (
    <>
      {showUpload && (
        <div className="mb-4 p-4 bg-surface rounded-lg border border-border">
          <Upload
            batch={false}
            autoUpload={false}
            onAdd={onAdd}
            restrictions={restrictions}
          />
        </div>
      )}
    </>
  );
};

export default ProfileUpload;