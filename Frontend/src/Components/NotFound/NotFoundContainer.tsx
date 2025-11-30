const NotFoundContainer = () => {
  return (
    <div className="min-h-[calc(100vh-4.05rem)] sm:min-h-[calc(100vh-4.55rem)] bg-background flex items-center justify-center px-4">
      <div className="max-w-lg w-full text-center">
        <div className="mb-8">
          <h1 className="text-9xl font-bold text-primary opacity-80">404</h1>
        </div>
        <div className="bg-card rounded-lg shadow-lg p-8 border border-border">
          <h2 className="text-3xl font-bold text-text-primary mb-4">
            Page Not Found
          </h2>
          <p className="text-text-secondary mb-8 text-lg">
            Sorry, the page you are looking for doesn't exist or has been moved.
          </p>
        </div>
      </div>
    </div>
  );
};

export default NotFoundContainer;
