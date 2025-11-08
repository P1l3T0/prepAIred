import useGenerateHrInterviews from "../../../Hooks/Interviews/HR/useGenerateHrInterviews";
import {
  aiAgentData,
  contextScenarioData,
  softSkillFocusData,
} from "../../../Utils/data";
import { Checkbox, NumericTextBox } from "@progress/kendo-react-inputs";
import { DropDownList, MultiSelect } from "@progress/kendo-react-dropdowns";
import FormField from "../Components/Common/FormField";
import GenerateButton from "../Components/Common/GenerateButton";

const GenerateHrInterviews = () => {
  const {
    handleDropDownChange,
    handleInputChange,
    handleGenerateHrInterviews,
    handleCheckBoxChange,
    isSubmitting,
  } = useGenerateHrInterviews();

  return (
    <div className="bg-background rounded-lg shadow-lg p-6 border border-border h-fit">
      <form onSubmit={handleGenerateHrInterviews} className="space-y-4">
        <FormField label="AI Agent" htmlFor="ai-agent">
          <DropDownList
            id="ai-agent"
            name="aiAgent"
            data={aiAgentData}
            size={"large"}
            defaultValue={"Select AI Agent"}
            onChange={handleDropDownChange}
          />
        </FormField>

        <FormField label="Soft Skills Focus" htmlFor="soft-skill-focus">
          <MultiSelect
            id="soft-skill-focus"
            name="softSkillFocus"
            data={softSkillFocusData}
            size={"large"}
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
            size={"large"}
            placeholder="Select interview scenarios..."
            onChange={handleDropDownChange}
          />
        </FormField>

        <FormField label="Number of Questions" htmlFor="number-of-questions-hr">
          <NumericTextBox
            id="number-of-questions-hr"
            name="numberOfQuestions"
            size={"large"}
            placeholder="Enter number (1-10)"
            min={1}
            max={10}
            onChange={handleInputChange}
          />
        </FormField>

        <FormField label="Prior Experience" htmlFor="prior-experience-hr">
          <Checkbox
            id="prior-experience-hr"
            name="hasPriorExperience"
            size={"large"}
            label="Do you have prior experience?"
            onChange={handleCheckBoxChange}
          />
        </FormField>

        <GenerateButton interviewType="HR" isSubmitting={isSubmitting} />
      </form>
    </div>
  );
};

export default GenerateHrInterviews;
