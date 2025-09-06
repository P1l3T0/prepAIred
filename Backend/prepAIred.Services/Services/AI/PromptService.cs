using prepAIred.Data;

namespace prepAIred.Services
{
    public class PromptService : IPromptService
    {
        public string CreatePrompt(AIRequestDTO aIRequest, int currentUserID)
        {
            string contentRules = $@"
                Act as an experienced technical interviewer conducting an interview for a {aIRequest.Level} {aIRequest.Topic} position.
                Generate {aIRequest.NumberOfQuestions} interview questions with detailed answers.

                Focus on:
                - Real-world scenarios and practical problems commonly encountered in {aIRequest.Topic} roles.
                - A mix of technical concepts, problem-solving approaches, and best practices relevant to a {aIRequest.Level} position.
                - Questions that are challenging and thought-provoking, requiring in-depth knowledge and critical thinking.
                - Comprehensive, well-explained answers that demonstrate deep understanding.
                - Coverage of algorithms, data structures, system design, and industry-specific technologies.

                Each question should have exactly 3 possible answers, one of which is correct.";

            string formatRules = @$"
                Format the response as strict JSON that can be deserialized into this class: 

                {{
                    ""userID"": {currentUserID}, // Use the provided currentUserID as the current User's ID
                    ""question"": """",
                    ""answersJson"": ""[]""
                }}

                Rules:
                - Return only raw JSON without Markdown code fences (do not include ``` or ```json).
                - Output must be a JSON array of InterviewDTO objects.
                - Property names must match exactly: Question, Answers, ID, DateCreated.
                - Each answer in the Answers array must be the full answer text only.
                - Do NOT prepend letters (A, B, C) or numbers (1, 2, 3) to the answers.
                - Do not add any extra properties outside this class.
                - No trailing commas in arrays or objects.
                - Strings must be properly escaped for JSON.
                - Do not include explanations, only raw JSON.";

            string prompt = string.Join(" ", contentRules, formatRules);

            return prompt.Trim();
        }
    }
}
