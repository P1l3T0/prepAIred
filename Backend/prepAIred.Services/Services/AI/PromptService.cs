using prepAIred.Data;

namespace prepAIred.Services
{
    public class PromptService : IPromptService
    {
        public string CreateHrPrompt(HrRequestDTO hrRequest, int currentUserID)
        {
            string contentRules = $@"
                Act as an experienced HR professional conducting a behavioral and competency-based interview for a position.
                Generate {hrRequest.NumberOfQuestions} interview questions with evaluation criteria.

                Content requirements:
                - Questions must follow the STAR (Situation, Task, Action, Result) interview methodology
                - Include a balanced mix of:
                    * Behavioral questions assessing past experiences
                    * Competency-based questions evaluating specific skills - {hrRequest.SoftSkillFocus}
                    * Cultural fit and values alignment questions
                    * Leadership and teamwork scenarios - {hrRequest.ContextScenario}
                - Each question must:
                    * Target specific soft skills and competencies
                    * Reveal candidate's problem-solving approach
                    * Assess interpersonal abilities
                    * Evaluate communication skills
                - For non-open-ended questions, include 3 sample responses reflecting:
                    * A strong answer demonstrating desired competencies
                    * A mediocre answer showing partial understanding
                    * A weak answer indicating lack of required skills";

            string formatRules = $@"
                Format requirements:
                - Output must be a **strict JSON array** of objects
                - Return only raw JSON without Markdown code fences
                - Each object must strictly follow this schema:

                {{
                    ""UserID"": {currentUserID},               // int - provided user ID
                    ""Question"": """",                       // string - the behavioral question
                    ""QuestionType"": 0,                     // int - 0 = SingleChoice, 1 = MultipleChoice, 2 = OpenEnded
                    ""QuestionType"": 0,                    // int - 0 = SingleChoice, 1 = MultipleChoice, 2 = OpenEnded
                    ""AnswersJson"": ""[]"",               // string - JSON array of answers as a string
                    ""CompetencyArea"": """",             // string - primary competency being assessed
                    ""BehavioralContext"": """"          // string - specific behavior or skill being evaluated
                    ""SoftSkillFocus"": {hrRequest.SoftSkillFocus}
                }}

                - Questions must be evenly distributed across:
                    * Leadership and management scenarios
                    * Team collaboration situations
                    * Conflict resolution cases
                    * Problem-solving scenarios
                    * Communication challenges
                - QuestionType must be an integer (0, 1, or 2)
                - AnswersJson must be a valid JSON array string with properly escaped quotes
                - No additional properties allowed
                - No nulls (use empty strings/arrays)
                - Output raw JSON only without explanations";

            string prompt = string.Join(" ", contentRules, formatRules);

            return prompt.Trim();
        }

        public string CreateTechnicalPrompt(TechnicalRequestDTO aIRequest, int currentUserID)
        {
            string contentRules = $@"
                Act as an experienced technical interviewer conducting an interview for a {aIRequest.DifficultyLevel} {aIRequest.Position} position.
                Generate {aIRequest.NumberOfQuestions} interview questions with detailed answers.

                Content requirements:
                - Questions must reflect real-world, practical scenarios commonly encountered in {aIRequest.Subject}.
                - Include a balanced mix of:
                    * Core technical concepts
                    * Problem-solving approaches
                    * Best practices relevant to a {aIRequest.DifficultyLevel} position
                    * Algorithms, data structures, system design, and industry-specific technologies
                - Each question must be challenging and thought-provoking, requiring in-depth reasoning.
                - Each non-open-ended question must have exactly 3 possible answers.
                - Answers must be full text only (no prefixes like A/B/C or 1/2/3).
                - One and only one answer must be correct (but do not label which is correct).
                - If the question type is OpenEnded, it must not include any answers.";

            string formatRules = @$"
                Format requirements:
                - Output must be a **strict JSON array** of objects.
                - Return only raw JSON without Markdown code fences (do not include ``` or ```json).
                - Each object must strictly follow this schema:

                {{
                    ""UserID"": {currentUserID},                                       // int - provided user ID
                    ""Question"": """",                                               // string - the interview question
                    ""QuestionType"": 0,                                             // int - 0 = SingleChoice, 1 = MultipleChoice, 2 = OpenEnded
                    ""InterviewType"": 1,                                           // int - 0 = HR, 1 = Technical
                    ""AnswersJson"": ""[]"",                                       // string - JSON array of answers as a string
                    ""ProgrammingLanguage"": ""{aIRequest.ProgrammingLanguage}"", // string - programming language focus
                    ""DifficultyLevel"": ""{aIRequest.DifficultyLevel}"",        // string - difficulty level of question
                    ""Subject"": ""{aIRequest.Subject}"",                       // string - technical topic area
                    ""Position"": ""{aIRequest.Position}""                     // string - position being interviewed for
                }}

                - Depending on the number of questions requested, the response must contain equal questions from all the possible question types.
                - Property names must match exactly: UserID, InterviewSessionID, Question, QuestionType, AnswersJson.
                - QuestionType must be an integer, not a string.
                - AnswersJson must always be a valid JSON array string:
                    * Example: ""[\""answer one\"", \""answer two\"", \""answer three\""]""
                - No additional properties are allowed.
                - No nulls allowed (use empty array []).
                - Strings must be properly escaped for JSON.
                - Do not include explanations, markdown, or code fences. Output raw JSON only.
                - No trailing commas in arrays or objects.";

            string prompt = string.Join(" ", contentRules, formatRules);

            return prompt.Trim();
        }
    }
}