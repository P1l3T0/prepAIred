import { defineConfig, loadEnv } from "vite";
import react from "@vitejs/plugin-react";
import tailwindcss from "@tailwindcss/vite";

export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd(), "");

  return {
    define: {
      "process.env.REACT_APP_API_URL": JSON.stringify(env.REACT_APP_API_URL),
      "process.env.ELEVENLABS_AGENT_ID": JSON.stringify(env.ELEVENLABS_AGENT_ID),
    },
    plugins: [react(), tailwindcss()],
  };
});
