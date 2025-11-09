import { useState } from "react";
import { type StepperChangeEvent, type StepProps } from "@progress/kendo-react-layout";
import { eyeIcon, trackChangesIcon } from "@progress/kendo-svg-icons";

const useChangeStep = () => {
  const items: StepProps[] = [
    { label: "HR Interview", svgIcon: eyeIcon },
    { label: "Technical Interview", svgIcon: trackChangesIcon },
  ];

  const [value, setValue] = useState<number>(0);

  const handleChange = (e: StepperChangeEvent) => {
    setValue(e.value);
  };

  return { items, value, handleChange };
};

export default useChangeStep;