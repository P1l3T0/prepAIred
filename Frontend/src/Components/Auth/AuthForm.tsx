import { useState } from "react";
import { TabStrip, TabStripTab } from "@progress/kendo-react-layout";
import type { TabStripSelectEventArguments } from "@progress/kendo-react-layout";
import RegisterForm from "./RegisterForm";
import LoginForm from "./LoginForm";

const AuthForm = () => {
  const [selected, setSelected] = useState<number>(0);

  const handleSelect = (e: TabStripSelectEventArguments) => {
    setSelected(e.selected);
  };

  return (
    <>
      <TabStrip selected={selected} onSelect={handleSelect}>
        <TabStripTab title="Login">
          <LoginForm />
        </TabStripTab>
        <TabStripTab title="Register">
          <RegisterForm />
        </TabStripTab>
      </TabStrip>
    </>
  );
};

export default AuthForm;