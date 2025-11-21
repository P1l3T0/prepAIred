import { Link } from "react-router-dom";

interface LinkButtonProps {
  isLogo?: boolean;
  to: string;
  label: string;
  onClick?: () => void;
}

const LinkButton = ({
  to,
  label,
  isLogo = false,
  onClick,
}: LinkButtonProps) => {
  return (
    <Link
      onClick={onClick}
      to={to}
      className={
        isLogo
          ? "text-text-primary text-2xl font-bold"
          : "text-text-primary hover:text-primary font-medium duration-200 p-3 rounded-md hover:bg-primary/10"
      }
    >
      {isLogo ? (
        <h1 className="text-2xl md:text-3xl font-bold text-text-primary mb-1">
          <span className="text-primary">prep</span>AIred
        </h1>
      ) : (
        label
      )}
    </Link>
  );
};

export default LinkButton;
