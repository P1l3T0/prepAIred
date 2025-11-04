import type { DropDownButtonItemClickEvent } from "@progress/kendo-react-buttons";
import { useTheme } from "../Context/Theme/useTheme";

interface DropDownButtonItem {
  text: string;
  value: string;
}

const useClickItem = () => {
  const { theme, toggleTheme } = useTheme();
  const items: DropDownButtonItem[] = [
    {
      text: "Light Mode",
      value: "light",
    },
    {
      text: "Dark Mode",
      value: "dark",
    },
  ];

  const onItemClick = (e: DropDownButtonItemClickEvent) => {
    toggleTheme(e.item.value);
  };

  return { items, theme, onItemClick };
};

export default useClickItem;
