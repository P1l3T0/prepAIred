import useGenerateTechnicalInterviews from "../../../Hooks/Interviews/Technical/useGenerateTechnicalInterviews";
import {
  aiAgentData,
  difficultyLevelData,
  positionData,
  programmingLanguageData,
  subjectData,
} from "../../../Utils/data";
import { NumericTextBox } from "@progress/kendo-react-inputs";
import { DropDownList, MultiSelect } from "@progress/kendo-react-dropdowns";
import FormField from "../Components/Common/FormField";
import GenerateButton from "../Components/Common/GenerateButton";

const GenerateTechnicalInterviews = () => {
  const {
    handleDropDownChange,
    handleInputChange,
    handleGenerateTechnicalInterviews,
    isSubmitting,
  } = useGenerateTechnicalInterviews();

  return (
    <div className="bg-inverse rounded-lg shadow-lg p-6 border border-border h-fit">
      <form onSubmit={handleGenerateTechnicalInterviews} className="space-y-4">
        <FormField label="AI Agent" htmlFor="ai-agent-tech">
          <DropDownList
            id="ai-agent-tech"
            name="aiAgent"
            data={aiAgentData}
            defaultValue={"Select AI Agent"}
            onChange={handleDropDownChange}
          />
        </FormField>

        <FormField label="Programming Language" htmlFor="programming-language">
          <DropDownList
            id="programming-language"
            name="programmingLanguage"
            data={programmingLanguageData}
            defaultValue={"Select Language"}
            onChange={handleDropDownChange}
          />
        </FormField>

        <FormField label="Difficulty Level" htmlFor="difficulty-level">
          <DropDownList
            id="difficulty-level"
            name="difficultyLevel"
            data={difficultyLevelData}
            defaultValue={"Select Difficulty Level"}
            onChange={handleDropDownChange}
          />
        </FormField>

        <FormField label="Position" htmlFor="position">
          <DropDownList
            id="position"
            name="position"
            data={positionData}
            defaultValue={"Select Position"}
            onChange={handleDropDownChange}
          />
        </FormField>

        <FormField label="Subject" htmlFor="subject">
          <MultiSelect
            id="subject"
            name="subject"
            data={subjectData}
            placeholder="Select technical subjects"
            onChange={handleDropDownChange}
          />
        </FormField>

        <FormField label="Number of Questions" htmlFor="number-of-questions-tech">
          <NumericTextBox
            id="number-of-questions-tech"
            name="numberOfQuestions"
            placeholder="Enter number (1-10)"
            min={1}
            max={10}
            onChange={handleInputChange}
          />
        </FormField>

        <GenerateButton interviewType="Technical" isSubmitting={isSubmitting} />
      </form>
    </div>
  );
};

export default GenerateTechnicalInterviews;
