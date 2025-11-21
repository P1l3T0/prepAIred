import { useState } from "react";
import { type StepProps } from "@progress/kendo-react-layout";
import { eyeIcon, trackChangesIcon } from "@progress/kendo-svg-icons";

const useChangeStep = () => {
  const items: StepProps[] = [
    { label: "HR Interview", svgIcon: eyeIcon },
    { label: "Technical Interview", svgIcon: trackChangesIcon },
  ];

  const [value, setValue] = useState<number>(0);

  const handleChangeStep = () => {
    setValue((prev) => prev === 0 ? 1 : 0);
  };

  return { items, value, handleChangeStep };
};

export default useChangeStep;