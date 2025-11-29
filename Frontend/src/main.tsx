import App from "./App.tsx";
import { createRoot } from "react-dom/client";
import { QueryClient, QueryClientProvider } from "react-query";
import { AuthProvider } from "./Context/Auth/AuthContext.tsx";
import { ThemeProvider } from "./Context/Theme/ThemeContext.tsx";
import InterviewGenerateButtonContextProvider from "./Context/InterviewGenerateButton/InterviewGenerateButtonContext.tsx";
import "./style.css";

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false,
      retry: 1
    },
  },
});

createRoot(document.getElementById("root")!).render(
  <QueryClientProvider client={queryClient}>
    <AuthProvider>
      <ThemeProvider>
        <InterviewGenerateButtonContextProvider>
          <App />
        </InterviewGenerateButtonContextProvider>
      </ThemeProvider>
    </AuthProvider>
  </QueryClientProvider>
);