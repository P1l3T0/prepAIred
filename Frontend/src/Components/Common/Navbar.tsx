import MobileMenuSidebar from "./MobileMenuSidebar";
import LogOutButton from "../Buttons/LogOutButton";
import ThemeButton from "../Buttons/ThemeButton";
import HamburgerButton from "../Buttons/HamburgerButtons";
import useToggleHamburgerButton from "../../Hooks/Buttons/useToggleHamburgerMenu";
import LinkButton from "../Buttons/LinkButton";

const Navbar = () => {
  const { isMobileMenuOpen, toggleMobileMenu } = useToggleHamburgerButton();

  return (
    <nav className="bg-background border-b border-border shadow-sm sticky top-0 z-50">
      <div className="px-4 lg:px-8">
        <div className="flex items-center justify-between py-3">
          <div className="shrink-0">
            <LinkButton to="/home" label="prepAIred" isLogo={true} />
          </div>

          <div className="hidden md:flex gap-3 items-center">
            <LinkButton to="/interviews" label="Interview Session" />
            <LinkButton to="/mock-interviews" label="Mock Interview" />
            <LinkButton to="/statistics" label="Statistics" />
          </div>

          <div className="hidden md:flex gap-3 items-center">
            <ThemeButton />
            <LogOutButton />
          </div>

          <div className="md:hidden">
            <HamburgerButton
              isHamburger={true}
              toggleMobileMenu={toggleMobileMenu}
            />
          </div>
        </div>
      </div>

      <MobileMenuSidebar
        isMobileMenuOpen={isMobileMenuOpen}
        toggleMobileMenu={toggleMobileMenu}
      />
    </nav>
  );
};

export default Navbar;
