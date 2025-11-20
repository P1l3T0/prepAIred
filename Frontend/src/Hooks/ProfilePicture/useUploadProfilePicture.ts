/**
 * Custom hook for handling profile picture upload functionality.
 * Manages file upload state, handles UI interactions, and submits upload requests.
 * On success, invalidates user queries to refresh profile data.
 * @returns {Object} - UI state and handlers for profile picture upload
 */
import axios, { AxiosError } from "axios";
import { useState } from "react";
import { useMutation, useQueryClient } from "react-query";
import { changeProfilePictureEndPoint } from "../../Utils/endpoints";
import type { UploadOnAddEvent } from '@progress/kendo-react-upload';

const useUploadProfilePicture = () => {
  const queryClient = useQueryClient();
  const [showUpload, setShowUpload] = useState(false);

  const handleAvatarClick = () => {
    setShowUpload((prev) => !prev);
  };

  const uploadFile = async (file: File) => {
    const formData: FormData = new FormData();
    formData.append("imageFile", file);

    await axios
      .post(changeProfilePictureEndPoint, formData, {
        withCredentials: true,
        headers: {
          "Content-Type": "multipart/form-data",
        },
      })
      .catch((err: AxiosError) => {
        debugger
        const error = err.response?.data as { title?: string };
        console.error(error?.title || "Upload failed");
        throw err;
      });
  };

  const { mutateAsync } = useMutation({
    mutationFn: uploadFile,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["profile-picture"] });
    }
  });

  const handleAdd = async (e: UploadOnAddEvent) => {
    const file = e.affectedFiles[0];

    if (file && file.getRawFile) {
      const rawFile = file.getRawFile();

      await mutateAsync(rawFile);
    }
  };

  return {
    showUpload,
    handleAvatarClick,
    handleAdd,
  };
};

export default useUploadProfilePicture;