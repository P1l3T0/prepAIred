import { Button } from "@progress/kendo-react-buttons";
import { useTheme } from "../Context/Theme/useTheme";
import { kpiStatusOpenIcon, circleShapeIcon } from "@progress/kendo-svg-icons";

const ThemeButton = () => {
  const { theme, toggleTheme } = useTheme();

  return (
    <>
      <Button
        togglable={true}
        onClick={toggleTheme}
        selected={theme === "dark"}
        svgIcon={theme === "light" ? circleShapeIcon : kpiStatusOpenIcon}
      />
    </>
  );
};

export default ThemeButton;
