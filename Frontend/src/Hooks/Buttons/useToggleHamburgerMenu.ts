import { useState } from "react";

const useToggleHamburgerMenu = () => {
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);

  const toggleMobileMenu = () => {
    setIsMobileMenuOpen((prev) => !prev);
  };

  return { isMobileMenuOpen, toggleMobileMenu };
};

export default useToggleHamburgerMenu;
