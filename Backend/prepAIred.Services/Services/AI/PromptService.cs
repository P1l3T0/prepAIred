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
                - For non-open-ended questions, include exactly 3 sample responses reflecting:
                    * A strong answer demonstrating desired competencies
                    * A mediocre answer showing partial understanding
                    * A weak answer indicating lack of required skills";

            string formatRules = $@"
                Format requirements:
                CRITICAL JSON FORMATTING RULES:
                1. The response MUST be a raw JSON array starting with [ and ending with ]
                2. The response MUST NOT have any ``` or ```json in the output
                3. DO NOT wrap the array in any additional object or property (no {{""response"":[], ""answers"":[], etc.}})
                4. Each array element must be an object with EXACTLY these properties:

                [
                    {{
                        ""UserID"": {currentUserID},              // int - provided user ID
                        ""Question"": """",                      // string - the behavioral question
                        ""QuestionType"": 0,                    // int - 0 = SingleChoice, 1 = MultipleChoice, 2 = OpenEnded
                        ""AnswersJson"": ""[]"",               // string - JSON array of answers as a string
                        ""CompetencyArea"": """",             // string - primary competency being assessed
                        ""BehavioralContext"": """"          // string - specific behavior or skill being evaluated
                        ""SoftSkillFocus"": {hrRequest.SoftSkillFocus}
                    }}
                ]

                STRICT REQUIREMENTS:
                - The outer structure MUST be a bare array [ ]
                - AnswersJson must be a string containing an escaped JSON array
                - All string values must use double quotes and be properly escaped
                - QuestionType values: 0 = SingleChoice, 1 = MultipleChoice, 2 = OpenEnded
                - Depending on the number of questions, ensure a balanced distribution of all the QuestionTypes
                - InterviewType must always be 0 for HR interviews
                - No additional properties
                - No null values (use empty strings """" instead)
                - No trailing commas
                - No comments in the output
                - No markdown fences (```
                - No explanations or text before or after the JSON array

                Example of correct AnswersJson format:
                ""[\""This is answer 1\"",\""This is answer 2\"",\""This is answer 3\""]""

                FORBIDDEN PATTERNS:
                - {{""response"": [...]}}  
                - {{""data"": [...]}}
                - {{""questions"": [...]}}  
                - Any wrapper object around the main array";

            string prompt = string.Join(" ", contentRules, formatRules);

            return prompt.Trim();
        }

        public string CreateTechnicalPrompt(TechnicalRequestDTO aIRequest, int currentUserID)
        {
            string contentRules = $@"
                Act as an experienced technical interviewer conducting an interview for a {aIRequest.DifficultyLevel} {aIRequest.Position} position.
                Generate {aIRequest.NumberOfQuestions} interview questions with detailed answers.

                Content requirements:
                - Questions must reflect real-world, practical scenarios commonly encountered in {aIRequest.Subject} using {aIRequest.ProgrammingLanguage}.
                - Include a balanced mix of:
                    * Core technical concepts specific to {aIRequest.ProgrammingLanguage}
                    * Problem-solving approaches
                    * Best practices relevant to a {aIRequest.DifficultyLevel} position
                    * Algorithms, data structures, system design, and industry-specific technologies
                - Each question must be challenging and thought-provoking, requiring in-depth reasoning.
                - Each non-open-ended question must have exactly 3 possible answers.
                - Answers must be full text only (no prefixes like A/B/C or 1/2/3).
                - One and only one answer must be correct (but do not label which is correct).
                - If the question type is OpenEnded, it must not include any answers.";

            string formatRules = $@"
                Format requirements:
                CRITICAL JSON FORMATTING RULES:
                1. The response MUST be a raw JSON array starting with [ and ending with ]
                2. DO NOT wrap the array in any additional object or property (no {{""response"":[], ""answers"":[], etc.}})
                3. Each array element must be an object with EXACTLY these properties:

                [
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
                ]

                STRICT REQUIREMENTS:
                - The outer structure MUST be a bare array [ ]
                - AnswersJson must be a string containing an escaped JSON array
                - All string values must use double quotes and be properly escaped
                - QuestionType values: 0 = SingleChoice, 1 = MultipleChoice, 2 = OpenEnded
                - Depending on the number of questions, ensure a balanced distribution of all the QuestionTypes
                - InterviewType must always be 1 for Technical interviews
                - No additional properties
                - No null values (use empty strings """" instead)
                - No trailing commas
                - No comments in the output
                - No markdown fences (``` or ```json) - this is the MOST IMPORTANT RULE!
                - No explanations or text before or after the JSON array
                - Questions must be evenly distributed across all QuestionTypes

                Example of correct AnswersJson format:
                ""[\""Implement a binary search algorithm using recursion\"",\""Use iteration instead of recursion\"",\""Use a linear search algorithm\""]""

                FORBIDDEN PATTERNS:
                - {{""response"": [...]}}  
                - {{""data"": [...]}}
                - {{""questions"": [...]}}  
                - Any wrapper object around the main array
                - Any additional properties not specified in the schema
                - Any unescaped quotes or newlines in strings";

            string prompt = string.Join(" ", contentRules, formatRules);

            return prompt.Trim();
        }
    }
}