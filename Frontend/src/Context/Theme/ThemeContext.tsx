import React, { createContext, useEffect, useState } from "react";

type Theme = "light" | "dark";

type ThemeContextType = {
  theme: Theme;
  setTheme: (theme: Theme) => void;
  toggleTheme: (theme: Theme) => void;
  kendoThemeUrl: string;
};

const KENDO_THEMES = {
  light: "https://unpkg.com/@progress/kendo-theme-bootstrap@12.2.1/dist/bootstrap-main.css",
  dark: "https://unpkg.com/@progress/kendo-theme-bootstrap@12.2.1/dist/bootstrap-4-dark.css",
};

export const ThemeContext = createContext<ThemeContextType | undefined>(undefined);

export const ThemeProvider = ({ children }: { children: React.ReactNode }) => {
  const [theme, setTheme] = useState<Theme>(() => {
    const stored = localStorage.getItem("theme") as Theme | null;
    if (stored) return stored;

    return window.matchMedia("(prefers-color-scheme: dark)").matches ? "dark" : "light";
  });

  const [kendoThemeUrl, setKendoThemeUrl] = useState<string>(() => {
    const stored = localStorage.getItem("theme") as Theme | null;
    return stored ? KENDO_THEMES[stored] : KENDO_THEMES[theme];
  });

  useEffect(() => {
    document.body.classList.toggle("dark", theme === "dark");
    localStorage.setItem("theme", theme);
    setKendoThemeUrl(KENDO_THEMES[theme]);
  }, [theme]);

  const toggleTheme = (theme: Theme) => setTheme(theme);

  return (
    <ThemeContext.Provider value={{ theme, setTheme, toggleTheme, kendoThemeUrl }}>
      <link rel="stylesheet" href={kendoThemeUrl} />
      {children}
    </ThemeContext.Provider>
  );
};
