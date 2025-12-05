import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import PersistLogin from "./Context/Auth/PersistLogin";
import ProtectedRoute from "./Context/Auth/ProtectedRoute";
import Navbar from "./Components/Common/Navbar";
import Home from "./Pages/Home";
import Interviews from "./Pages/Interviews";
import useAuth from "./Context/Auth/useAuth";
import Login from "./Pages/Login";
import Register from "./Pages/Register";
import Footer from "./Components/Common/Footer/Footer";
import NotFound from "./Pages/NotFound";

function App() {
  const { isUserLoggedIn } = useAuth();

  return (
    <BrowserRouter>
      {isUserLoggedIn ? <Navbar /> : null}
      <Routes>
        <Route element={<PersistLogin />}>
          <Route path="*" element={<NotFound />} />
          <Route path="/" element={<Navigate to="/login" replace />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
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
      {isUserLoggedIn ? <Footer /> : null}
    </BrowserRouter>
  );
}

export default App;
