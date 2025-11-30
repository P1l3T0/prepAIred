import { Upload, type UploadOnAddEvent } from "@progress/kendo-react-upload";

interface ProfileUploadProps {
  showUpload: boolean;
  onAdd: (e: UploadOnAddEvent) => void;
}

const ProfileUpload = ({ showUpload, onAdd }: ProfileUploadProps) => {
  return (
    <>
      {showUpload && (
        <div className="bg-surface rounded-lg border p-2 border-border">
          <Upload
            batch={false}
            autoUpload={false}
            onAdd={onAdd}
            restrictions={{
              allowedExtensions: [".jpg", ".jpeg", ".png", ".gif"],
              maxFileSize: 2 * 1024 * 1024
            }}
          />
        </div>
      )}
    </>
  );
};

export default ProfileUpload;