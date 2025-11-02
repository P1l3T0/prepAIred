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
import { Button } from "@progress/kendo-react-buttons";

const GenerateTechnicalInterviews = () => {
  const {
    handleDropDownChange,
    handleInputChange,
    handleGenerateTechnicalInterviews,
    disabled,
    isSubmitting,
  } = useGenerateTechnicalInterviews();

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

      <DropDownList
        id="programming-language"
        name="programmingLanguage"
        data={programmingLanguageData}
        label="Programming Language"
        style={{ width: "500px" }}
        onChange={handleDropDownChange}
      />

      <br />

      <DropDownList
        id="difficulty-level"
        name="difficultyLevel"
        data={difficultyLevelData}
        label="Difficulty Level"
        style={{ width: "500px" }}
        onChange={handleDropDownChange}
      />

      <br />

      <DropDownList
        id="position"
        name="position"
        data={positionData}
        label="Position"
        style={{ width: "500px" }}
        onChange={handleDropDownChange}
      />

      <br />

      <MultiSelect
        id="subject"
        name="subject"
        data={subjectData}
        label="Subject"
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

      <Button
        id="tech-button"
        disabled={disabled || isSubmitting}
        onClick={handleGenerateTechnicalInterviews}
      >
        {isSubmitting ? "Generating..." : "Generate Technical Interviews"}
      </Button>
    </>
  );
};

export default GenerateTechnicalInterviews;
