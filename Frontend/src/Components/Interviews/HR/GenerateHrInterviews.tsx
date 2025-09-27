import useGenerateHrInterviews from "../../../Hooks/Interviews/HR/useGenerateHrInterviews";

const GenerateHrInterviews = () => {
  const { handleSelectChange, handleInputChange, handleGenerateHrInterviews } = useGenerateHrInterviews();

  return (
    <>
      <label htmlFor="ai-agent">AI Agent:</label>
      <select name="aiAgent" id="ai-agent" onChange={handleSelectChange}>
        <option value="ChatGPT">ChatGPT</option>
        <option value="Gemini">Gemini</option>
        <option value="Claude">Claude</option>
      </select>

      <br />

      <label htmlFor="soft-skill-focus">Soft Skill Focus:</label>
      <select name="softSkillFocus" id="soft-skill-focus" onChange={handleSelectChange}>
        <option value="Communication">Communication</option>
        <option value="Problem-Solving">Problem-Solving</option>
        <option value="Teamwork">Teamwork</option>
      </select>

      <br />

      <label htmlFor="context-scenario">Context Scenario:</label>
      <select name="contextScenario" id="context-scenario" onChange={handleSelectChange}>
        <option value="Team Collaboration">Team Collaboration</option>
        <option value="Conflict Resolution">Conflict Resolution</option>
        <option value="Leadership">Leadership</option>
      </select>

      <br />

      <label htmlFor="number-of-questions">Number of Questions:</label>
      <input type="number" name="numberOfQuestions" onChange={handleInputChange} />

      <br />

      <button onClick={handleGenerateHrInterviews}>Generate HR Interviews</button>
    </>
  );
};

export default GenerateHrInterviews;
