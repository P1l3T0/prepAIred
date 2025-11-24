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
import { Card, CardBody } from "@progress/kendo-react-layout";
import useInterviewGenerateButton from "../../../Context/InterviewGenerateButton/useInterviewGenerateButton";

const GenerateHrInterviews = () => {
  const { disableHrInterviewButton } = useInterviewGenerateButton();
  const {
    handleDropDownChange,
    handleInputChange,
    handleGenerateHrInterviews,
    handleCheckBoxChange,
    isSubmitting,
  } = useGenerateHrInterviews();

  return (
    <Card className="shadow-sm shadow-primary">
      <CardBody>
        <form onSubmit={handleGenerateHrInterviews} className="space-y-4">
          <FormField label="AI Agent" htmlFor="ai-agent">
            <DropDownList
              id="ai-agent"
              name="aiAgent"
              data={aiAgentData}
              size={"medium"}
              defaultValue={"Select AI Agent"}
              onChange={handleDropDownChange}
            />
          </FormField>

          <FormField label="Soft Skills Focus" htmlFor="soft-skill-focus">
            <MultiSelect
              id="soft-skill-focus"
              name="softSkillFocus"
              data={softSkillFocusData}
              size={"medium"}
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
              size={"medium"}
              placeholder="Select interview scenarios..."
              onChange={handleDropDownChange}
            />
          </FormField>

          <FormField label="Number of Questions" htmlFor="number-of-questions-hr">
            <NumericTextBox
              id="number-of-questions-hr"
              name="numberOfQuestions"
              size={"medium"}
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
              size={"medium"}
              label="Do you have prior experience?"
              onChange={handleCheckBoxChange}
            />
          </FormField>

          <GenerateButton interviewType="HR" isSubmitting={isSubmitting} disabled={disableHrInterviewButton} />
        </form>
      </CardBody>
    </Card>
  );
};

export default GenerateHrInterviews;
