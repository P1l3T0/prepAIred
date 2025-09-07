import useGenerateAiInterviews from "../../Hooks/Interviews/useGenerateAiInterviews";

const AiInterviews = () => {
  const { handleSelectChange, handleInputChange, handleGenerateAiInterviews } = useGenerateAiInterviews();

  return (
    <>
      <select name="aiAgent" id="ai-agent" onChange={handleSelectChange}>
        <option value="ChatGPT">ChatGPT</option>
        <option value="Gemini">Gemini</option>
        <option value="Claude">Claude</option>
      </select>

      <select name="field" id="field" onChange={handleSelectChange}>
        <option value="c#-oop">OOP</option>
        <option value="data-scientist">Data Scientist</option>
        <option value="product-manager">Product Manager</option>
      </select>

      <select name="level" id="level" onChange={handleSelectChange}>
        <option value="entry-level">Entry Level</option>
        <option value="mid-level">Mid Level</option>
        <option value="senior-level">Senior Level</option>
      </select>

      <input type="number" name="numberOfQuestions" onChange={handleInputChange} />
      <button onClick={handleGenerateAiInterviews}>Generate AI Interviews</button>
    </>
  );
};

export default AiInterviews;