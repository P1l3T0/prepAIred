import type { FormRenderProps } from "@progress/kendo-react-form";
import { Form, Field, FormElement, FieldWrapper, FormFieldSet, FormSeparator } from "@progress/kendo-react-form";
import { Button } from "@progress/kendo-react-buttons";
import useLogin from "../../Hooks/Auth/useLogin";
import useInputValidations from "./useInputValidations";

const LoginForm = () => {
  const { handleSubmit, handleChange } = useLogin();
  const { emailValidator, EmailInput, passwordValidator, PasswordInput } = useInputValidations();

  return (
    <Form
      onSubmit={handleSubmit}
      render={(formRenderProps: FormRenderProps) => (
        <FormElement>
          <FormFieldSet>
            <FieldWrapper>
              <Field
                name={"email"}
                type={"email"}
                label={"Email"}
                component={EmailInput}
                validator={emailValidator}
                onChange={handleChange}
              />
            </FieldWrapper>
            <FieldWrapper>
              <div className="k-form-field-wrap">
                <Field
                  name={"password"}
                  labelClassName={"k-form-label"}
                  label={"Password"}
                  type={"password"}
                  component={PasswordInput}
                  validator={passwordValidator}
                  onChange={handleChange}
                />
              </div>
            </FieldWrapper>
          </FormFieldSet>
          <FormSeparator />
          <div className="k-form-buttons">
            <Button disabled={!formRenderProps.allowSubmit}>Login</Button>
          </div>
        </FormElement>
      )}
    />
  );
};

export default LoginForm;
