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

                Content requirements:
                - Questions must reflect real-world, practical scenarios commonly encountered in {aIRequest.Topic} roles.
                - Include a balanced mix of:
                    * Core technical concepts
                    * Problem-solving approaches
                    * Best practices relevant to a {aIRequest.Level} position
                    * Algorithms, data structures, system design, and industry-specific technologies
                - Each question must be challenging and thought-provoking, requiring in-depth reasoning.
                - Each non-open-ended question must have exactly 3 possible answers.
                - Answers must be full text only (no prefixes like A/B/C or 1/2/3).
                - One and only one answer must be correct (but do not label which is correct).
                - If the question type is OpenEnded, it must not include any answers.
            ";

            string formatRules = @$"
                Format requirements:
                - Output must be a **strict JSON array** of objects.
                - Return only raw JSON without Markdown code fences (do not include ``` or ```json).
                - Each object must strictly follow this schema:

                {{
                    ""UserID"": {currentUserID},  // int - provided user ID
                    ""Question"": """",           // string - the interview question
                    ""QuestionType"": 0,          // int - 0 = SingleChoice, 1 = MultipleChoice, 2 = OpenEnded
                    ""AnswersJson"": ""[]""       // string - JSON array of answers as a string
                }}

                - Property names must match exactly: UserID, InterviewSessionID, Question, QuestionType, AnswersJson.
                - QuestionType must be an integer, not a string.
                - AnswersJson must always be a valid JSON array string:
                    * Example: ""[\""answer one\"", \""answer two\"", \""answer three\""]""
                - No additional properties are allowed.
                - No nulls allowed (use empty string "" or empty array [] if needed).
                - Strings must be properly escaped for JSON.
                - Do not include explanations, markdown, or code fences. Output raw JSON only.
                - No trailing commas in arrays or objects.";

            string prompt = string.Join(" ", contentRules, formatRules);

            return prompt.Trim();
        }
    }
}