interface HamburgerButtonProps {
  isHamburger: boolean;
  toggleMobileMenu: () => void;
}

const HamburgerButton = ({
  isHamburger,
  toggleMobileMenu,
}: HamburgerButtonProps) => {
  return (
    <>
      <button
        onClick={toggleMobileMenu}
        className="sm:hidden text-text-primary p-2 rounded-md hover:bg-gray-400 focus:outline-none"
      >
        {isHamburger ? (
          <svg
            className="w-6 h-6"
            fill="none"
            stroke="currentColor"
            viewBox="0 0 24 24"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth={2}
              d="M4 6h16M4 12h16M4 18h16"
            />
          </svg>
        ) : (
          <svg
            className="w-6 h-6"
            fill="none"
            stroke="currentColor"
            viewBox="0 0 24 24"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth={2}
              d="M6 18L18 6M6 6l12 12"
            />
          </svg>
        )}
      </button>
    </>
  );
};

export default HamburgerButton;
