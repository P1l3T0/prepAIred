interface AuthFormProps {
  children: React.ReactNode;
}

const AuthForm = ({ children }: AuthFormProps) => {
  return (
    <main className="bg-background">
      <div className="min-h-screen flex items-center justify-center p-6">
        <div className="w-full max-w-md">
          <div className="text-center mb-8">
            <h1 className="text-4xl font-bold text-text-primary mb-2">
              <span className="text-primary">prep</span>AIred
            </h1>
            <p className="text-text-secondary text-sm">
              Prepare for your next interview with AI
            </p>
          </div>
          <div className="bg-surface border border-border rounded-lg shadow-xl p-6 sm:p-8">
            {children}
          </div>
        </div>
      </div>
    </main>
  );
};

export default AuthForm;
