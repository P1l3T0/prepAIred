using prepAIred.Data;
using prepAIred.Exceptions;
using System.ClientModel;
using System.Text.Json;
using OpenAI.Chat;
using GenerativeAI;
using GenerativeAI.Types;
using Microsoft.Extensions.Configuration;

namespace prepAIred.Services
{
    public class AIService(IConfiguration configuration) : IAIService
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<List<Interview>> AskAiAgentAsync(AIAgent aiAgent, string prompt)
        {
            List<Interview> interviews = await (aiAgent switch
            {
                AIAgent.ChatGPT => AskChatGPTAsync(prompt),
                AIAgent.Gemini => AskGeminiAsync(prompt),
                AIAgent.Claude => AskClaudeAsync(prompt),
                _ => throw new UnsupportedAiAgentException($"Unsupported AI agent: {aiAgent}")
            });

            return interviews;
        }

        public string CreatePrompt(AIRequestDTO aIRequest, int currentUserID)
        {
            string contentRules = $@"
                Act as an experienced technical interviewer conducting an interview for a {aIRequest.Level} {aIRequest.Field} position.
                Generate {aIRequest.NumberOfQuestions} interview questions with detailed answers.

                Focus on:
                - Real-world scenarios and practical problems commonly encountered in {aIRequest.Field} roles.
                - A mix of technical concepts, problem-solving approaches, and best practices relevant to a {aIRequest.Level} position.
                - Questions that are challenging and thought-provoking, requiring in-depth knowledge and critical thinking.
                - Comprehensive, well-explained answers that demonstrate deep understanding.
                - Coverage of algorithms, data structures, system design, and industry-specific technologies.

                Each question should have exactly 3 possible answers, one of which is correct.";

            string formatRules = @$"
                Format the response as strict JSON that can be deserialized into this class: 

                {{
                    ""userID"": {currentUserID},
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

        private async Task<List<Interview>> AskChatGPTAsync(string prompt)
        {
            string apiKey = _configuration.GetSection("Appsettings:OpenAIAPIKEY").Value!;
            string model = _configuration.GetSection("Appsettings:OpenAIModel").Value!;

            ChatClient chatClient = new ChatClient(model, apiKey);
            ClientResult<ChatCompletion> completion = await chatClient.CompleteChatAsync(prompt);
            string response = completion.Value.Content[0].Text.Trim();

            return DeserializeInterviews(response);
        }

        private Task<List<Interview>> AskClaudeAsync(string prompt)
        {
            throw new NotImplementedException();
        }

        private async Task<List<Interview>> AskGeminiAsync(string prompt)
        {
            string apiKey = _configuration.GetSection("Appsettings:GeminiAPIKEY").Value!;
            string model = _configuration.GetSection("Appsettings:GeminiModel").Value!;

            GenerativeModel generativeModel = new GenerativeModel(apiKey, model);
            GenerateContentResponse aiResponse = await generativeModel.GenerateContentAsync(prompt);
            string response = aiResponse.Text.Trim();

            return DeserializeInterviews(response);
        }

        private List<Interview> DeserializeInterviews(string response)
        {
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<Interview> interviews = JsonSerializer.Deserialize<List<Interview>>(response, options) ?? new List<Interview>();

            foreach (Interview interview in interviews)
            {
                interview.ID = 0;
            }

            return interviews;
        }
    }
}