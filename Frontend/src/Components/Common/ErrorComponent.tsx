import { Card, CardBody } from "@progress/kendo-react-layout";

const ErrorComponent = () => {
  return (
    <div className="bg-background flex items-center justify-center p-4">
      <Card className="shadow-lg">
        <CardBody>
          <div className="text-center p-8">
            <h2 className="text-xl text-text-primary font-semibold mb-2">
              Something went wrong
            </h2>
            <p className="text-text-secondary">
              Please try refreshing the page
            </p>
          </div>
        </CardBody>
      </Card>
    </div>
  );
};

export default ErrorComponent;