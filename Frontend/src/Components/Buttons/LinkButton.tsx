import { Link } from "react-router-dom";

interface LinkButtonProps {
  isLogo?: boolean;
  to: string;
  label: string;
}

const LinkButton = ({ to, label, isLogo = false }: LinkButtonProps) => {
  return (
    <Link
      to={to}
      className={
        isLogo
          ? "text-text-primary text-2xl font-bold"
          : "text-text-primary hover:text-primary font-medium duration-200 p-3 rounded-md hover:bg-primary/10"
      }
    >
      {isLogo ? <span className="text-primary">{label}</span> : label}
    </Link>
  );
};

export default LinkButton;
