import type { FormRenderProps } from "@progress/kendo-react-form";
import {
  Form,
  Field,
  FormElement,
  FieldWrapper,
  FormFieldSet,
} from "@progress/kendo-react-form";
import useLogin from "../../Hooks/Auth/useLogin";
import useInputValidations from "./useInputValidations";
import FormButton from "./FormButton";

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

          <FormButton to="/register" text="Log In" formRenderProps={formRenderProps} />
        </FormElement>
      )}
    />
  );
};

export default LoginForm;
