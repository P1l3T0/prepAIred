import type { FormRenderProps } from "@progress/kendo-react-form";
import { Form, Field, FormElement, FieldWrapper, FormFieldSet, FormSeparator } from "@progress/kendo-react-form";
import { Input } from "@progress/kendo-react-inputs";
import { Button } from "@progress/kendo-react-buttons";
import useRegister from "../../Hooks/Auth/useRegister";
import useInputValidations from "./useInputValidations";

const RegisterForm = () => {
  const { handleSubmit, handleChange } = useRegister();
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
                  name={"username"}
                  labelClassName={"k-form-label"}
                  label={"Username"}
                  component={Input}
                  onChange={handleChange}
                />
              </div>
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
            <Button disabled={!formRenderProps.allowSubmit}>Register</Button>
          </div>
        </FormElement>
      )}
    />
  );
};

export default RegisterForm;
