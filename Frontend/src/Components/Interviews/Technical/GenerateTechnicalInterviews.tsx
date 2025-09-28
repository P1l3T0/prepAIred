import useGenerateTechnicalInterviews from "../../../Hooks/Interviews/Technical/useGenerateTechnicalInterviews";

const GenerateTechnicalInterviews = () => {
  const { handleSelectChange, handleInputChange, handleGenerateTechnicalInterviews } = useGenerateTechnicalInterviews();

  return (
    <>
      <label htmlFor="ai-agent">AI Agent:</label>
      <select name="aiAgent" id="ai-agent" onChange={handleSelectChange}>
        <option value="ChatGPT">ChatGPT</option>
        <option value="Gemini">Gemini</option>
        <option value="Claude">Claude</option>
      </select>

      <br />

      <label htmlFor="programming-language">Programming Language:</label>
      <select name="programmingLanguage" id="programming-language" onChange={handleSelectChange}>
        <option value="C#">C#</option>
        <option value="JavaScript">JavaScript</option>
        <option value="Python">Python</option>
        <option value="Java">Java</option>
      </select>

      <br />

      <label htmlFor="subject">Subject:</label>
      <select name="subject" id="subject" onChange={handleSelectChange}>
        <option value="Object-Oriented Programming">Object-Oriented Programming</option>
        <option value="Data Structures">Data Structures</option>
        <option value="Algorithms">Algorithms</option>
        <option value="Databases">Databases</option>
      </select>

      <br />

      <label htmlFor="difficulty-level">Difficulty Level:</label>
      <select name="difficultyLevel" id="difficulty-level" onChange={handleSelectChange}>
        <option value="Easy">Easy</option>
        <option value="Medium">Medium</option>
        <option value="Hard">Hard</option>
      </select>

      <br />

      <label htmlFor="position">Position:</label>
      <select name="position" id="position" onChange={handleSelectChange}>
        <option value="Junior Developer">Junior Developer</option>
        <option value="Mid Developer">Mid Developer</option>
        <option value="Senior Developer">Senior Developer</option>
      </select>

      <br />

      <label htmlFor="number-of-questions">Number of Questions:</label>
      <input id="number-of-questions" type="number" name="numberOfQuestions" min={1} max={10} defaultValue={3} onChange={handleInputChange} />

      <br />

      <button onClick={handleGenerateTechnicalInterviews}>Generate Technical Interviews</button>
    </>
  );
};

export default GenerateTechnicalInterviews;
