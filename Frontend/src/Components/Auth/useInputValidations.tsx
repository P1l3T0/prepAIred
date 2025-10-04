import { Error } from "@progress/kendo-react-labels";
import { Input } from "@progress/kendo-react-inputs";
import type { FieldRenderProps } from "@progress/kendo-react-form";

const emailRegex: RegExp = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
const passwordRegex: RegExp = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{10,}$/;

const emailValidator = (value: string) => emailRegex.test(value) ? "" : "Please enter a valid email.";
const passwordValidator = (value: string) => passwordRegex.test(value) ? "" : "Password must be at least 10 characters, include uppercase, lowercase, number, and special character.";

const EmailInput = (fieldRenderProps: FieldRenderProps) => {
  const { validationMessage, visited, ...others } = fieldRenderProps;
  return (
    <div className="k-form-field-wrap">
      <Input {...others} labelClassName={"k-form-label"} />
      {visited && validationMessage && <Error>{validationMessage}</Error>}
    </div>
  );
};

const PasswordInput = (fieldRenderProps: FieldRenderProps) => {
  const { validationMessage, visited, ...others } = fieldRenderProps;
  return (
    <div className="k-form-field-wrap">
      <Input {...others} type="password" labelClassName={"k-form-label"} />
      {visited && validationMessage && <Error>{validationMessage}</Error>}
    </div>
  );
};

const useInputValidations = () => ({
  emailValidator,
  EmailInput,
  passwordValidator,
  PasswordInput,
});

export default useInputValidations;