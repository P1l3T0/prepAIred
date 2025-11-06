import { Button } from "@progress/kendo-react-buttons";
import useChangeTheme from "../../Hooks/Buttons/useChangeTheme";

const ThemeButton = () => {
  const { theme, handleClick } = useChangeTheme();

  return (
    <Button
      value={theme}
      fillMode={"flat"}
      themeColor={"primary"}
      onClick={handleClick}
      iconClass={`${
        theme === "light" ? "fa-solid fa-sun" : "fa-solid fa-moon"
      }`}
    />
  );
};

export default ThemeButton;
