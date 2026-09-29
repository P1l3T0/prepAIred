import { Loader } from "@progress/kendo-react-indicators";

const LoaderComponent = () => {
  return (
    <div className="bg-background flex items-center justify-center">
      <div className="flex flex-col items-center space-y-4">
        <Loader size="large" />
        <span className="text-text-secondary">Loading...</span>
      </div>
    </div>
  );
};

export default LoaderComponent;