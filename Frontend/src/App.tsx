import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import AuthenticationForms from "./Pages/AuthenticationForms";
import PersistLogin from "./Context/Auth/PersistLogin";
import ProtectedRoute from "./Context/Auth/ProtectedRoute";
import Home from "./Pages/Home";

function App() {
  return (
    <BrowserRouter>
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
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
