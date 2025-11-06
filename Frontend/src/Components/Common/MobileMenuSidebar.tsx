import HamburgerButton from "../Buttons/HamburgerButtons";
import LinkButton from "../Buttons/LinkButton";
import LogOutButton from "../Buttons/LogOutButton";
import ThemeButton from "../Buttons/ThemeButton";

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
      className={`fixed top-0 right-0 h-full bg-background shadow-lg z-10000 duration-200 ease-in-out md:hidden ${
        isMobileMenuOpen ? "translate-x-0" : "translate-x-full"
      }`}
    >
      <div className="flex flex-col h-full p-4">
        <div className="flex justify-end items-center mb-4">
          <HamburgerButton
            isHamburger={false}
            toggleMobileMenu={toggleMobileMenu}
          />
        </div>

        <div className="flex flex-col gap-4 items-center">
          <LinkButton to="/interviews" label="New Interview" />
          <LinkButton to="/mock-interviews" label="Mock Interview" />
          <LinkButton to="/statistics" label="Statistics" />
        </div>

        <div className="mt-auto flex flex-col gap-4">
          <ThemeButton />
          <LogOutButton />
        </div>
      </div>
    </div>
  );
};

export default MobileMenuSidebar;
