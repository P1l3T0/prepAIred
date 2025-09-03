import useLogin from '../Hooks/Auth/useLogin';

const Login = () => {
  const { handleSubmit, handleChange } = useLogin();

  return (
    <>
      <form method="post" autoComplete="off">
        <div>
          <label htmlFor="email">Email</label>
          <input type="email" id="email" name="email" onChange={handleChange} required />
        </div>
        <div>
          <label htmlFor="password">Password:</label>
          <input type="password" id="password" name="password" onChange={handleChange} required /> 
        </div>

        <button onClick={handleSubmit}>Login</button>
      </form>
    </>
  )
}

export default Login;