import type { FormRenderProps } from "@progress/kendo-react-form";
import {
  Form,
  Field,
  FormElement,
  FieldWrapper,
  FormFieldSet,
} from "@progress/kendo-react-form";
import useRegister from "../../Hooks/Auth/useRegister";
import useInputValidations from "../Auth/useInputValidations";
import FormButton from "../Buttons/FormButton";

const RegisterForm = () => {
  const { handleSubmit, handleChange } = useRegister();
  const { emailValidator, EmailInput, passwordValidator, PasswordInput, UsernameInput } = useInputValidations();

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

          <FormButton to="/login" text="Create Account" formRenderProps={formRenderProps} />
        </FormElement>
      )}
    />
  );
};

export default RegisterForm;
