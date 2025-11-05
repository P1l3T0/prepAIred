import { DropDownButton } from "@progress/kendo-react-buttons";
import { kpiStatusOpenIcon, circleShapeIcon } from "@progress/kendo-svg-icons";
import useClickItem from "../../Hooks/Buttons/useClickItem";

const ThemeButton = () => {
  const { items, theme, onItemClick } = useClickItem();

  return (
    <DropDownButton
      items={items}
      text="Change Theme"
      onItemClick={onItemClick}
      svgIcon={theme === "light" ? circleShapeIcon : kpiStatusOpenIcon}
    />
  );
};

export default ThemeButton;
