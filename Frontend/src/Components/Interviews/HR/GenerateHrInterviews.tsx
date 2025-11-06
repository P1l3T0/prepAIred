import useGenerateHrInterviews from "../../../Hooks/Interviews/HR/useGenerateHrInterviews";
import {
  aiAgentData,
  contextScenarioData,
  softSkillFocusData,
} from "../../../Utils/data";
import { NumericTextBox } from "@progress/kendo-react-inputs";
import { DropDownList, MultiSelect } from "@progress/kendo-react-dropdowns";
import FormField from "../Common/FormField";
import SubmitButton from "../Common/SubmitButton";

const GenerateHrInterviews = () => {
  const {
    handleDropDownChange,
    handleInputChange,
    handleGenerateHrInterviews,
    isSubmitting,
  } = useGenerateHrInterviews();

  return (
    <div className="bg-inverse rounded-lg shadow-lg p-6 border border-border h-fit">
      <form onSubmit={handleGenerateHrInterviews} className="space-y-4">
        <FormField label="AI Agent" htmlFor="ai-agent">
          <DropDownList
            id="ai-agent"
            name="aiAgent"
            data={aiAgentData}
            defaultValue={"Select AI Agent"}
            onChange={handleDropDownChange}
          />
        </FormField>

        <FormField label="Soft Skills Focus" htmlFor="soft-skill-focus">
          <MultiSelect
            id="soft-skill-focus"
            name="softSkillFocus"
            data={softSkillFocusData}
            placeholder="Select soft skills to focus on..."
            onChange={handleDropDownChange}
          />
        </FormField>

        <FormField label="Context Scenarios" htmlFor="context-scenario">
          <MultiSelect
            id="context-scenario"
            name="contextScenario"
            data={contextScenarioData}
            autoClose={true}
            placeholder="Select interview scenarios..."
            onChange={handleDropDownChange}
          />
        </FormField>

        <FormField label="Number of Questions" htmlFor="number-of-questions">
          <NumericTextBox
            id="number-of-questions"
            name="numberOfQuestions"
            placeholder="Enter number (1-10)"
            min={1}
            max={10}
            onChange={handleInputChange}
          />
        </FormField>

        <SubmitButton id="hr-button" isSubmitting={isSubmitting} />
      </form>
    </div>
  );
};

export default GenerateHrInterviews;
