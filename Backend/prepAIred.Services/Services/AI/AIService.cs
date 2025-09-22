using prepAIred.Data;
using prepAIred.Exceptions;
using Anthropic.SDK;
using Anthropic.SDK.Messaging;
using GenerativeAI;
using GenerativeAI.Types;
using OpenAI.Chat;
using System.Text.Json;
using System.ClientModel;
using Microsoft.Extensions.Configuration;

namespace prepAIred.Services
{
    public class AIService(IConfiguration configuration) : IAIService
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<List<Interview>> AskAiAgentAsync<TInterview>(AIAgent aiAgent, string prompt) where TInterview : Interview
        {
            List<Interview> interviews = await (aiAgent switch
            {
                AIAgent.ChatGPT => AskChatGPTAsync<TInterview>(prompt),
                AIAgent.Gemini => AskGeminiAsync<TInterview>(prompt),
                AIAgent.Claude => AskClaudeAsync<TInterview>(prompt),
                _ => throw new UnsupportedAiAgentException($"Unsupported AI agent: {aiAgent}")
            });

            return interviews;
        }

        private async Task<List<Interview>> AskChatGPTAsync<TInterview>(string prompt) where TInterview : Interview
        {
            string apiKey = _configuration.GetSection("Appsettings:OpenAIAPIKEY").Value!;
            string model = _configuration.GetSection("Appsettings:OpenAIModel").Value!;

            ChatClient chatClient = new ChatClient(model, apiKey);
            ClientResult<ChatCompletion> completion = await chatClient.CompleteChatAsync(prompt);
            string response = completion.Value.Content[0].Text.Trim();

            return DeserializeInterviews<TInterview>(response);
        }

        private async Task<List<Interview>> AskClaudeAsync<TInterview>(string prompt) where TInterview : Interview
        {
            string apiKey = _configuration.GetSection("Appsettings:ClaudeAPIKEY").Value!;
            string model = _configuration.GetSection("Appsettings:ClaudeModel").Value!;

            AnthropicClient client = new AnthropicClient(apiKey);
            MessageParameters parameters = new MessageParameters()
            {
                Model = model,
                MaxTokens = 1024,
                Messages = new List<Message>()
                {
                    new Message()
                    {
                        Role = RoleType.User,
                        Content = new List<ContentBase>()
                        {
                            new TextContent() 
                            { 
                                Text = prompt
                            }
                        }
                    }
                }
            };

            MessageResponse aiResponse = await client.Messages.GetClaudeMessageAsync(parameters);
            string response = string.Join("\n", aiResponse.Content.OfType<TextContent>().Select(c => c.Text));

            return DeserializeInterviews<TInterview>(response);
        }

        private async Task<List<Interview>> AskGeminiAsync<TInterview>(string prompt) where TInterview : Interview
        {
            string apiKey = _configuration.GetSection("Appsettings:GeminiAPIKEY").Value!;
            string model = _configuration.GetSection("Appsettings:GeminiModel").Value!;

            GenerativeModel generativeModel = new GenerativeModel(apiKey, model);
            GenerateContentResponse aiResponse = await generativeModel.GenerateContentAsync(prompt);
            string response = aiResponse.Text.Trim();

            return DeserializeInterviews<TInterview>(response);
        }

        private List<Interview> DeserializeInterviews<TInterview>(string response) where TInterview : Interview
        {
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return typeof(TInterview) == typeof(HRInterview)
                ? JsonSerializer.Deserialize<List<HRInterview>>(response, options)?.Cast<Interview>().ToList() ?? new List<Interview>()
                : typeof(TInterview) == typeof(TechnicalInterview)
                    ? JsonSerializer.Deserialize<List<TechnicalInterview>>(response, options)?.Cast<Interview>().ToList() ?? new List<Interview>()
                    : [];
        }
    }
}