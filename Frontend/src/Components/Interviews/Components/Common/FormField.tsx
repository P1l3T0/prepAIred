interface FormFieldProps {
  label: string;
  htmlFor: string;
  children: React.ReactNode;
}

const FormField = ({ label, htmlFor, children }: FormFieldProps) => {
  return (
    <div className="form-group">
      <label
        htmlFor={htmlFor}
        className="block text-lg font-medium text-text-primary mb-2"
      >
        {label}
      </label>
      {children}
    </div>
  );
};

export default FormField;
