using prepAIred.Data;

namespace prepAIred.Services
{
    public interface IPromptService
    {
        /// <summary>
        /// Creates a prompt string for the AI agent based on the request and user ID.
        /// </summary>
        /// <param name="aIRequest">The request DTO containing interview parameters.</param>
        /// <param name="currentUserID">The ID of the current user.</param>
        /// <returns>The generated prompt string.</returns>
        string CreateTechnicalPrompt(TechnicalRequestDTO aIRequest, int currentUserID);

        /// <summary>
        /// Creates a prompt tailored for human resources (HR) based on the provided AI request data and the current
        /// user's ID.
        /// </summary>
        /// <param name="aIRequest">The AI request data containing the context and parameters for generating the HR prompt.</param>
        /// <param name="currentUserID">The unique identifier of the current user. Must be a valid, non-negative integer.</param>
        /// <returns>A string representing the generated HR prompt.</returns>
        string CreateHrPrompt(HrRequestDTO aIRequest, int currentUserID);
    }
}
