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
        string CreatePrompt(AIRequestDTO aIRequest, int currentUserID);
    }
}
