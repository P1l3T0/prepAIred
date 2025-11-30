interface HamburgerButtonProps {
  isHamburger: boolean;
  toggleMobileMenu: () => void;
}

const HamburgerButton = ({ isHamburger, toggleMobileMenu }: HamburgerButtonProps) => {
  return (
    <button
      onClick={toggleMobileMenu}
      className="md:hidden text-text-primary p-2 rounded-md hover:bg-primary/10 hover:text-primary ease-in-out duration-200"
    >
      {isHamburger ? (
        <i className="fa-solid fa-bars"></i>
      ) : (
        <i className="fa-solid fa-x"></i>
      )}
    </button>
  );
};

export default HamburgerButton;
