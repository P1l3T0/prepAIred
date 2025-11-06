import { Error } from "@progress/kendo-react-labels";
import { Input } from "@progress/kendo-react-inputs";
import type { FieldRenderProps } from "@progress/kendo-react-form";

const emailRegex: RegExp = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
const passwordRegex: RegExp = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{10,}$/;

const emailValidator = (value: string) => emailRegex.test(value) ? "" : "Please enter a valid email.";
const passwordValidator = (value: string) => passwordRegex.test(value) ? "" : "Password must be at least 10 characters, include uppercase, lowercase, number, and special character.";

const EmailInput = (fieldRenderProps: FieldRenderProps) => {
  const { validationMessage, visited, ...rest } = fieldRenderProps;
  return (
    <div className="k-form-field-wrap">
      <Input {...rest} autoComplete="off" label="" placeholder="Email" />
      {visited && validationMessage && <Error>{validationMessage}</Error>}
    </div>
  );
};

const PasswordInput = (fieldRenderProps: FieldRenderProps) => {
  const { validationMessage, visited, ...rest } = fieldRenderProps;
  return (
    <div className="k-form-field-wrap">
      <Input
        {...rest}
        autoComplete="off"
        label=""
        placeholder="Password"
        type="password"
      />
      {visited && validationMessage && <Error>{validationMessage}</Error>}
    </div>
  );
};

const UsernameInput = (fieldRenderProps: FieldRenderProps) => {
  const { validationMessage, visited, ...rest } = fieldRenderProps;
  return (
    <div className="k-form-field-wrap">
      <Input {...rest} autoComplete="off" placeholder="Username" />
      {visited && validationMessage && <Error>{validationMessage}</Error>}
    </div>
  );
};

const useInputValidations = () => ({
  emailValidator,
  EmailInput,
  passwordValidator,
  PasswordInput,
  UsernameInput,
});

export default useInputValidations;
