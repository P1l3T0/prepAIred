import LogOutButton from "./LogOutButton";
import ThemeButton from "./ThemeButton";
import HamburgerButton from "./HamburgerButtons";
import MobileMenuSidebar from "./MobileMenuSidebar";
import useToggleHamburgerButton from "../../Hooks/Buttons/useToggleHamburgerMenu";

const Navbar = () => {
  const { isMobileMenuOpen, toggleMobileMenu } = useToggleHamburgerButton();

  return (
    <>
      <nav className="bg-background p-4">
        <div className="flex justify-between items-center">
          <h1 className="text-text-primary text-2xl font-bold">prepAIred</h1>
          <div className="hidden sm:flex gap-2 items-center">
            <ThemeButton />
            <LogOutButton />
          </div>
          <HamburgerButton isHamburger={true} toggleMobileMenu={toggleMobileMenu} />
        </div>
        <MobileMenuSidebar
          isMobileMenuOpen={isMobileMenuOpen}
          toggleMobileMenu={toggleMobileMenu}
        />
      </nav>
    </>
  );
};

export default Navbar;
