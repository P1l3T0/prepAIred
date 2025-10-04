import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import Home from "./Pages/Home";
import PersistLogin from "./Context/PersistLogin";
import ProtectedRoute from "./Context/ProtectedRoute";
import AuthenticationForms from "./Pages/AuthenticationForms";

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
