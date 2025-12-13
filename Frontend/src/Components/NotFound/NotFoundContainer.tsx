import useAuth from "../../Context/Auth/useAuth";

const NotFoundContainer = () => {
  const { isUserLoggedIn } = useAuth();

  return (
    <main className="bg-background">
      <div className={`${isUserLoggedIn ? "min-h-[calc(100vh-4rem)]" : "min-h-screen"} flex items-center justify-center px-4`}>
        <div className="max-w-lg w-full text-center">
          <div className="bg-surface rounded-lg shadow-lg p-8 border border-border">
            <div className="mb-8">
              <h1 className="text-7xl md:text-9xl font-bold text-primary opacity-80">
                404
              </h1>
            </div>
            <h2 className="text-2xl md:text-3xl font-bold text-text-primary mb-4">
              Page Not Found
            </h2>
            <p className="text-text-secondary mb-8 text-md md:text-lg">
              Sorry, the page you are looking for doesn't exist or has been
              moved.
            </p>
          </div>
        </div>
      </div>
    </main>
  );
};

export default NotFoundContainer;
