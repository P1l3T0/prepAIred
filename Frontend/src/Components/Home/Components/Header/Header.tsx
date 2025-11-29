interface HeaderProps {
  username: string;
}

const Header = ({ username }: HeaderProps) => {
  return (
    <div className="bg-linear-to-r from-primary/20 to-secondary/10 border-border">
      <div className="max-w-7xl mx-auto px-6 py-8 flex flex-col items-center justify-center space-y-6">
        <h1 className="text-3xl text-center font-bold text-text-primary mb-2">
          Welcome back, {username}!
        </h1>
        <p className="text-text-secondary text-lg">
          Continue your interview preparation journey
        </p>
      </div>
    </div>
  );
};

export default Header;
