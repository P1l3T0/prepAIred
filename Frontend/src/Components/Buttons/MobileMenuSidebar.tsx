import HamburgerButton from "./HamburgerButtons";
import LogOutButton from "./LogOutButton";
import ThemeButton from "./ThemeButton";

interface MobileMenuSidebarProps {
  isMobileMenuOpen: boolean;
  toggleMobileMenu: () => void;
}

const MobileMenuSidebar = ({
  isMobileMenuOpen,
  toggleMobileMenu,
}: MobileMenuSidebarProps) => {
  return (
    <div
      className={`fixed top-0 right-0 h-full bg-background shadow-lg z-10000 duration-200 ease-in-out sm:hidden ${
        isMobileMenuOpen ? "translate-x-0" : "translate-x-full"
      }`}
    >
      <div className="flex flex-col p-4 gap-4">
        <div className="flex justify-end items-center mb-4">
          <HamburgerButton
            isHamburger={false}
            toggleMobileMenu={toggleMobileMenu}
          />
        </div>
        <ThemeButton />
        <LogOutButton />
      </div>
    </div>
  );
};

export default MobileMenuSidebar;
