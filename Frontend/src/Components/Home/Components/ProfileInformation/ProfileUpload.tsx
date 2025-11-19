import { Upload, type UploadOnAddEvent } from "@progress/kendo-react-upload";

interface ProfileUploadProps {
  showUpload: boolean;
  isUploading: boolean;
  onAdd: (e: UploadOnAddEvent) => void;
}

const ProfileUpload = ({
  showUpload,
  isUploading,
  onAdd,
}: ProfileUploadProps) => {
  return (
    <>
      {showUpload && (
        <div className="mb-4 p-4 bg-surface rounded-lg border border-border">
          <Upload
            batch={false}
            disabled={isUploading}
            onAdd={onAdd}
          />
          <div className="flex gap-2 mt-3">
            {isUploading && (
              <span className="text-text-secondary text-sm flex items-center">
                <span className="animate-spin inline-block w-4 h-4 border-2 border-current border-t-transparent rounded-full mr-2"></span>
                Uploading...
              </span>
            )}
          </div>
        </div>
      )}
    </>
  );
};

export default ProfileUpload;