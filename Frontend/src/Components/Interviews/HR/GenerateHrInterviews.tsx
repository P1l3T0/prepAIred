import useGenerateHrInterviews from "../../../Hooks/Interviews/HR/useGenerateHrInterviews";
import { aiAgentData, contextScenarioData, softSkillFocusData } from "../../../Utils/data";
import { NumericTextBox } from "@progress/kendo-react-inputs";
import { DropDownList, MultiSelect } from "@progress/kendo-react-dropdowns";
import { Button } from "@progress/kendo-react-buttons";

const GenerateHrInterviews = () => {
  const { handleDropDownChange, handleInputChange, handleGenerateHrInterviews } = useGenerateHrInterviews();

  return (
    <>
      <DropDownList
        id="ai-agent"
        name="aiAgent"
        data={aiAgentData}
        label="AI Agent"
        style={{ width: "500px" }}
        onChange={handleDropDownChange}
      />

      <br />

      <MultiSelect
        id="soft-skill-focus"
        name="softSkillFocus"
        data={softSkillFocusData}
        label="Soft Skill Focus"
        style={{ width: "500px" }}
        onChange={handleDropDownChange}
      />

      <br />

      <MultiSelect
        id="context-scenario"
        name="contextScenario"
        data={contextScenarioData}
        autoClose={true}
        label="Context Scenario"
        style={{ width: "500px" }}
        onChange={handleDropDownChange}
      />

      <br />

      <NumericTextBox
        id="number-of-questions"
        name="numberOfQuestions"
        placeholder="Number of Questions"
        min={1}
        max={10}
        defaultValue={3}
        style={{ width: "500px" }}
        onChange={handleInputChange}
      />

      <br />

      <Button id="hr-button" onClick={handleGenerateHrInterviews}>
        Generate HR Interviews
      </Button>
    </>
  );
};

export default GenerateHrInterviews;
