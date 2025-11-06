import useGenerateHrInterviews from "../../../Hooks/Interviews/HR/useGenerateHrInterviews";
import {
  aiAgentData,
  contextScenarioData,
  softSkillFocusData,
} from "../../../Utils/data";
import { NumericTextBox } from "@progress/kendo-react-inputs";
import { DropDownList, MultiSelect } from "@progress/kendo-react-dropdowns";
import { Button } from "@progress/kendo-react-buttons";
import { Loader } from "@progress/kendo-react-indicators";

const GenerateHrInterviews = () => {
  const {
    handleDropDownChange,
    handleInputChange,
    handleGenerateHrInterviews,
    disabled,
    isSubmitting,
  } = useGenerateHrInterviews();

  return (
    <div className="bg-card rounded-lg shadow-lg p-6 border border-border h-fit">
      <form onSubmit={handleGenerateHrInterviews} className="space-y-4">
        <div className="form-group">
          <label
            htmlFor="ai-agent"
            className="block text-sm font-medium text-text-primary mb-2"
          >
            AI Agent
          </label>
          <DropDownList
            id="ai-agent"
            name="aiAgent"
            data={aiAgentData}
            defaultValue={"ChatGPT"}
            style={{ width: "100%" }}
            onChange={handleDropDownChange}
          />
        </div>

        <div className="form-group">
          <label
            htmlFor="soft-skill-focus"
            className="block text-sm font-medium text-text-primary mb-2"
          >
            Soft Skills Focus
          </label>
          <MultiSelect
            id="soft-skill-focus"
            name="softSkillFocus"
            data={softSkillFocusData}
            placeholder="Select soft skills to focus on..."
            style={{ width: "100%" }}
            onChange={handleDropDownChange}
          />
        </div>

        <div className="form-group">
          <label
            htmlFor="context-scenario"
            className="block text-sm font-medium text-text-primary mb-2"
          >
            Context Scenarios
          </label>
          <MultiSelect
            id="context-scenario"
            name="contextScenario"
            data={contextScenarioData}
            autoClose={true}
            placeholder="Select interview scenarios..."
            style={{ width: "100%" }}
            onChange={handleDropDownChange}
          />
        </div>

        <div className="form-group">
          <label
            htmlFor="number-of-questions"
            className="block text-sm font-medium text-text-primary mb-2"
          >
            Number of Questions
          </label>
          <NumericTextBox
            id="number-of-questions"
            name="numberOfQuestions"
            placeholder="Enter number (1-10)"
            min={1}
            max={10}
            defaultValue={3}
            style={{ width: "100%" }}
            onChange={handleInputChange}
          />
        </div>

        <div className="pt-4">
          <Button
            id="hr-button"
            themeColor="primary"
            fillMode={"outline"}
            size="large"
            disabled={disabled || isSubmitting}
            onClick={handleGenerateHrInterviews}
            style={{ width: "100%" }}
          >
            {isSubmitting ? (
              <>
              <Loader type={"infinite-spinner"} className="mr-2" />
                Generating...
              </>
            ) : (
              <>
                <span className="mr-2"></span>
                Generate Questions
              </>
            )}
          </Button>

          {disabled && (
            <div className="mt-4 p-3 bg-success/10 border border-success rounded-lg">
              <p className="text-sm text-success text-center font-medium">
                ✓ Questions generated successfully!
              </p>
            </div>
          )}
        </div>
      </form>
    </div>
  );
};

export default GenerateHrInterviews;
