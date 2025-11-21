using prepAIred.Data;

namespace prepAIred.Services
{
    public class PromptService(ISerializationService serializationService) : IPromptService
    {
        private readonly ISerializationService _serializationService = serializationService;

        public string CreateHrPrompt(HrRequestDTO hrRequest, int currentUserID)
        {
            string contentRules = $@"
                Act as an experienced HR professional conducting a behavioral and competency-based interview for a position.
                Generate {hrRequest.NumberOfQuestions} interview questions with evaluation criteria.
                Generate the questions based on whether or not the candidate has prior experience: {hrRequest.HasPriorExperience}.

                Content requirements:
                - Questions must follow the STAR (Situation, Task, Action, Result) interview methodology
                - Include a balanced mix of:
                    * Behavioral questions assessing past experiences
                    * Competency-based questions evaluating specific skills - {string.Join(", ", hrRequest.SoftSkillFocus)}
                    * Cultural fit and values alignment questions
                    * Leadership and teamwork scenarios - {string.Join(", ", hrRequest.ContextScenario)}
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
                        ""UserID"": {currentUserID},                                                                     // int - provided user ID
                        ""Question"": """",                                                                             // string - the behavioral question
                        ""QuestionType"": 0,                                                                           // int - 0 = SingleChoice, 1 = MultipleChoice, 2 = OpenEnded
                        ""AnswersJson"": ""[]"",                                                                      // string - JSON array of answers as a string
                        ""CompetencyArea"": """",                                                                    // string - primary competency being assessed
                        ""BehavioralContext"": """"                                                                 // string - specific behavior or skill being evaluated
                        ""SoftSkillFocus"": Use 1 of the following - {string.Join(", ", hrRequest.SoftSkillFocus)} // string - soft skill focus area
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

        public string CreateHrEvaluationPrompt(List<EvaluateRequestDTO> evaluateRequest)
        {
            string serializedRequests = _serializationService.SerializeCollection(evaluateRequest);

            string contentRules = $@"
                Act as an experienced HR professional evaluating candidate responses to behavioral and competency-based interview questions.
                Provide a score and detailed feedback for each response in {serializedRequests} based on the STAR (Situation, Task, Action, Result) interview methodology.

                CRITICAL MATCHING RULE:
                - For each question at index N in the input array, your response must set the ""SelectedAnswer"" field to EXACTLY match the Answer field from the corresponding request at index N in {serializedRequests}
                - Failure to match the SelectedAnswer to the corresponding Answer will result in rejection of your response.

                Evaluation criteria:
                - Assess how well the response demonstrates the required competencies and soft skills
                - Evaluate clarity, relevance, and depth of the answer
                - Consider alignment with company values and culture
                - Provide constructive feedback highlighting strengths and areas for improvement
                - Score each response on a scale of 1 to 5, where 1 = Poor, 3 = Average, 5 = Excellent";

            string formatRules = @"
                Format requirements:
                CRITICAL JSON FORMATTING RULES:
                1. The response MUST be a raw JSON array starting with [ and ending with ]
                2. The response MUST NOT have any ``` or ```json in the output
                3. DO NOT wrap the array in any additional object or property (no {""response"":[], ""answers"":[], etc.})
                4. Each array element must be an object with EXACTLY these properties:

                [
                    {
                        ""ID"": 0,                                // int - ID of the interview (use 0 if not available, otherwise try to use the same IDs as in the serialized interviews)
                        ""UserID"": 0,                           // int - ID of the user this interview belongs to
                        ""InterviewSessionID"": 0,              // int - ID of the interview session
                        ""Question"": """",                    // string - the interview question
                        ""IsAnswered"": true,                 // boolean - whether the question has been answered (always true for evaluations)
                        ""SelectedAnswer"": """",            // string - the answer selected by the user
                        ""Score"": 0,                       // float - score from 0 to 10 (0 = Poor, 5 = Average, 10 = Excellent). You as the evaluator will assign this score based on the quality of the SelectedAnswer
                        ""Feedback"": """",                // string - detailed feedback on the answer
                        ""AnswersJson"": ""[]"",          // string - JSON array of possible answers as a string
                        ""QuestionType"": 0,             // int - 0 = SingleChoice, 1 = MultipleChoice, 2 = OpenEnded
                        ""InterviewType"": 0,           // int - 0 = HR, 1 = Technical (always 0 for HR interviews)
                        ""SoftSkillFocus"": """",      // string - primary soft skill being assessed
                        ""CompetencyArea"": """",     // string - specific competency being evaluated
                        ""BehavioralContext"": """"  // string - behavioral context of the question
                    }
                ]

                STRICT REQUIREMENTS:
                - The outer structure MUST be a bare array [ ]
                - All string values must use double quotes and be properly escaped
                - Score must be an integer between 1 and 5
                - No additional properties
                - No null values (use empty strings """" instead)
                - No trailing commas
                - No comments in the output
                - No markdown fences (``` or ```json) - this is the MOST IMPORTANT RULE!
                - No explanations or text before or after the JSON array
                FORBIDDEN PATTERNS:
                - {""response"": [...]}
                - {""data"": [...]}
                - {""evaluations"": [...]}
                - Any wrapper object around the main array";

            string prompt = string.Join(" ", contentRules, formatRules);
            return prompt.Trim();
        }

        public string CreateTechnicalPrompt(TechnicalRequestDTO techRequest, int currentUserID)
        {
            string contentRules = $@"
                Act as an experienced technical interviewer conducting an interview for a {techRequest.DifficultyLevel} {techRequest.Position} position.
                Generate {techRequest.NumberOfQuestions} interview questions with detailed answers.
                Generate the questions based on whether or not the candidate has prior experience: {techRequest.HasPriorExperience}.

                Content requirements:
                - Questions must reflect real-world, practical scenarios commonly encountered in {string.Join(", ", techRequest.Subject)} using {techRequest.ProgrammingLanguage}.
                - Include a balanced mix of:
                    * Core technical concepts specific to {techRequest.ProgrammingLanguage}
                    * Problem-solving approaches
                    * Best practices relevant to a {techRequest.DifficultyLevel} position
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
                        ""UserID"": {currentUserID},                                                                  // int - provided user ID
                        ""Question"": """",                                                                          // string - the interview question
                        ""QuestionType"": 0,                                                                        // int - 0 = SingleChoice, 1 = MultipleChoice, 2 = OpenEnded
                        ""InterviewType"": 1,                                                                      // int - 0 = HR, 1 = Technical
                        ""AnswersJson"": ""[]"",                                                                  // string - JSON array of answers as a string
                        ""ProgrammingLanguage"": ""{techRequest.ProgrammingLanguage}""                           // string - programming language focus
                        ""DifficultyLevel"": ""{techRequest.DifficultyLevel}"",                                 // string - difficulty level of question
                        ""Subject"": ""Use 1 of the following - {string.Join(", ", techRequest.Subject)}"",    // string - technical topic area
                        ""Position"": ""{techRequest.Position}""                                              // string - position being interviewed for
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

        public string CreateTechnicalEvaluationPrompt(List<EvaluateRequestDTO> evaluateRequest)
        {
            string serializedRequests = _serializationService.SerializeCollection(evaluateRequest);

            string contentRules = $@"
                Act as an experienced technical interviewer evaluating candidate responses to technical interview questions.
                Provide detailed evaluation for each response in {serializedRequests} based on technical accuracy, problem-solving approach, and coding best practices.

                Evaluation criteria:
                - Assess technical accuracy and completeness of the solution
                - Evaluate problem-solving methodology and approach
                - Consider code quality, efficiency, and best practices
                - Review understanding of the specific programming language concepts
                - Analyze solution scalability and performance considerations
                - Provide constructive feedback highlighting strengths and areas for improvement
                - Score each response on a scale of 0 to 10, where:
                    * 0-2 = Poor (Incorrect or severely lacking)
                    * 3-4 = Fair (Partially correct with significant gaps)
                    * 5-6 = Average (Correct but basic implementation)
                    * 7-8 = Good (Correct with good practices)
                    * 9-10 = Excellent (Optimal solution with best practices)";

            string formatRules = @"
                Format requirements:
                CRITICAL JSON FORMATTING RULES:
                1. The response MUST be a raw JSON array starting with [ and ending with ]
                2. The response MUST NOT have any ``` or ```json in the output
                3. DO NOT wrap the array in any additional object or property
                4. Each array element must be an object with EXACTLY these properties:

                [
                    {
                        ""ID"": 0,                                  // int - ID of the interview (use 0 if not available, otherwise try to use the same IDs as in the serialized interviews)
                        ""UserID"": 0,                             // int - ID of the user this interview belongs to
                        ""InterviewSessionID"": 0,                // int - ID of the interview session
                        ""Question"": """",                      // string - the technical question
                        ""IsAnswered"": true,                   // boolean - always true for evaluations
                        ""SelectedAnswer"": """",              // string - the candidate's submitted answer
                        ""Score"": 0,                         // float - score from 0 to 10
                        ""Feedback"": """",                  // string - detailed technical feedback
                        ""AnswersJson"": ""[]"",            // string - JSON array of possible answers
                        ""QuestionType"": 0,               // int - 0 = SingleChoice, 1 = MultipleChoice, 2 = OpenEnded
                        ""InterviewType"": 1,             // int - must be 1 for Technical interviews
                        ""ProgrammingLanguage"": """",   // string - language being evaluated
                        ""DifficultyLevel"": """",      // string - difficulty level of the question
                        ""Subject"": """",             // string - technical topic area
                        ""Position"": """"            // string - position being interviewed for
                    }
                ]

                STRICT REQUIREMENTS:
                - The outer structure MUST be a bare array [ ]
                - All string values must use double quotes and be properly escaped
                - Score must be between 0 and 10
                - No additional properties
                - No null values (use empty strings """" instead)
                - No trailing commas
                - No comments in the output
                - No markdown fences (``` or ```json)
                - No explanations or text before or after the JSON array
                FORBIDDEN PATTERNS:
                - {""response"": [...]}
                - {""data"": [...]}
                - {""evaluations"": [...]}
                - Any wrapper object around the main array";

            string prompt = string.Join(" ", contentRules, formatRules);
            return prompt.Trim();
        }

        public string GetPromptWithSerializedInterviews(string prompt, string serializedInterviews)
        {
            return $"{prompt}\n\nHere are the interviews in JSON format:\n{serializedInterviews}";
        }
    }
}