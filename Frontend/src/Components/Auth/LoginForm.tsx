import type { FormRenderProps } from "@progress/kendo-react-form";
import {
  Form,
  Field,
  FormElement,
  FieldWrapper,
  FormFieldSet,
} from "@progress/kendo-react-form";
import { Button } from "@progress/kendo-react-buttons";
import useLogin from "../../Hooks/Auth/useLogin";
import useInputValidations from "./useInputValidations";
import { Link } from "react-router";

const LoginForm = () => {
  const { handleSubmit, handleChange } = useLogin();
  const { emailValidator, EmailInput, passwordValidator, PasswordInput } = useInputValidations();

  return (
    <Form
      onSubmit={handleSubmit}
      render={(formRenderProps: FormRenderProps) => (
        <FormElement className="space-y-6">
          <FormFieldSet className="space-y-4">
            <FieldWrapper>
              <Field
                name={"email"}
                type={"email"}
                label={"Email"}
                component={EmailInput}
                validator={emailValidator}
                onChange={handleChange}
                className="w-full"
              />
            </FieldWrapper>
            <FieldWrapper>
              <Field
                name={"password"}
                label={"Password"}
                type={"password"}
                component={PasswordInput}
                validator={passwordValidator}
                onChange={handleChange}
                className="w-full"
              />
            </FieldWrapper>
          </FormFieldSet>

          <div className="mt-10">
            <Button
              fillMode={"outline"}
              themeColor={"primary"}
              className="w-full mb-5"
              disabled={!formRenderProps.allowSubmit}
            >
              Sign In
            </Button>
            <p className="text-center text-sm text-text-secondary">
              Don't have an account?{" "}
              <Link
                to="/register"
                className="text-primary font-medium cursor-pointer hover:underline"
              >
                Sign up
              </Link>
            </p>
          </div>
        </FormElement>
      )}
    />
  );
};

export default LoginForm;
