import type { FormRenderProps } from "@progress/kendo-react-form";
import {
  Form,
  Field,
  FormElement,
  FieldWrapper,
  FormFieldSet,
} from "@progress/kendo-react-form";
import { Button } from "@progress/kendo-react-buttons";
import useRegister from "../../Hooks/Auth/useRegister";
import useInputValidations from "./useInputValidations";
import { Link } from "react-router";

const RegisterForm = () => {
  const { handleSubmit, handleChange } = useRegister();
  const {
    emailValidator,
    EmailInput,
    passwordValidator,
    PasswordInput,
    UsernameInput,
  } = useInputValidations();

  return (
    <Form
      onSubmit={handleSubmit}
      render={(formRenderProps: FormRenderProps) => (
        <FormElement className="space-y-6">
          <FormFieldSet className="space-y-4">
            <FieldWrapper>
              <Field
                name={"username"}
                component={UsernameInput}
                onChange={handleChange}
              />
            </FieldWrapper>
            <FieldWrapper>
              <Field
                name={"email"}
                type={"email"}
                component={EmailInput}
                validator={emailValidator}
                onChange={handleChange}
              />
            </FieldWrapper>
            <FieldWrapper>
              <Field
                name={"password"}
                type={"password"}
                component={PasswordInput}
                validator={passwordValidator}
                onChange={handleChange}
              />
            </FieldWrapper>
          </FormFieldSet>

          <div className="my-5">
            <Button
              fillMode={"outline"}
              themeColor={"primary"}
              className="w-full mb-5"
              disabled={!formRenderProps.allowSubmit}
            >
              Create Account
            </Button>
            <p className="text-center text-sm text-text-secondary">
              Already have an account?{" "}
              <Link
                to="/login"
                className="text-primary font-medium cursor-pointer hover:underline"
              >
                Sign in
              </Link>
            </p>
          </div>
        </FormElement>
      )}
    />
  );
};

export default RegisterForm;
