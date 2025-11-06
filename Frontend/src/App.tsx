import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import AuthenticationForms from "./Pages/AuthenticationForms";
import PersistLogin from "./Context/Auth/PersistLogin";
import ProtectedRoute from "./Context/Auth/ProtectedRoute";
import Navbar from "./Components/Common/Navbar";
import Home from "./Pages/Home";
import Interviews from "./Pages/Interviews";
import useAuth from "./Context/Auth/useAuth";

function App() {
  const { isUserLoggedIn } = useAuth();
  
  return (
    <BrowserRouter>
      {isUserLoggedIn ? <Navbar /> : null}
      <Routes>
        <Route element={<PersistLogin />}>
          <Route path="/" element={<Navigate to="/authenticate" replace />} />
          <Route path="/authenticate" element={<AuthenticationForms />} />
          <Route
            path="/home"
            element={
              <ProtectedRoute>
                <Home />
              </ProtectedRoute>
            }
          />
          <Route
            path="/interviews"
            element={
              <ProtectedRoute>
                <Interviews />
              </ProtectedRoute>
            }
          />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
