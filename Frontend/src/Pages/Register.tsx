import useRegister from "../Hooks/Auth/useRegister";

const Register = () => {
  const { handleSubmit, handleChange } = useRegister();

  return (
    <>
      <form method="post" autoComplete="off">
        <div>
          <label htmlFor="email">Email</label>
          <input type="email" id="email" name="email" onChange={handleChange} required />
        </div>
        <div>
          <label htmlFor="username">Username:</label>
          <input type="text" id="username" name="username" onChange={handleChange} required />
        </div>
        <div>
          <label htmlFor="password">Password:</label>
          <input type="password" id="password" name="password" onChange={handleChange} required /> 
        </div>

        <button onClick={handleSubmit}>Register</button>
      </form>
    </>
  );
};

export default Register;
