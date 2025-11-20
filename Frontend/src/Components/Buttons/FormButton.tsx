import { Button } from "@progress/kendo-react-buttons";
import type { FormRenderProps } from "@progress/kendo-react-form";
import { Link } from "react-router-dom";

interface FormButtonProps {
  to: string;
  text: string;
  formRenderProps: FormRenderProps;
}

const FormButton = ({ formRenderProps, to, text }: FormButtonProps) => {
  return (
    <>
      <div className="mt-10">
        <Button
          fillMode={"outline"}
          themeColor={"primary"}
          className="w-full mb-5"
          disabled={!formRenderProps.allowSubmit}
        >
          {text}
        </Button>
        <p className="text-center text-sm text-text-secondary">
          {text === "Log In"
            ? "Don't have an account? "
            : "Already have an account? "}
          <Link
            to={to}
            className="text-primary font-medium cursor-pointer hover:underline"
          >
            {text === "Log In" ? "Sign up" : "Log in"}
          </Link>
        </p>
      </div>
    </>
  );
};

export default FormButton;
