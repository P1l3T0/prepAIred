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

        public async Task<List<Interview>> AskAiAgentAsync<TInterview>(AIAgent aiAgent, string prompt) where TInterview : class
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

        private async Task<List<Interview>> AskChatGPTAsync<T>(string prompt) where T : class
        {
            string apiKey = _configuration.GetSection("Appsettings:OpenAIAPIKEY").Value!;
            string model = _configuration.GetSection("Appsettings:OpenAIModel").Value!;

            ChatClient chatClient = new ChatClient(model, apiKey);
            ClientResult<ChatCompletion> completion = await chatClient.CompleteChatAsync(prompt);
            string response = completion.Value.Content[0].Text.Trim();

            return DeserializeInterviews<T>(response);
        }

        private async Task<List<Interview>> AskClaudeAsync<T>(string prompt) where T : class
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

            return DeserializeInterviews<T>(response);
        }

        private async Task<List<Interview>> AskGeminiAsync<T>(string prompt) where T : class
        {
            string apiKey = _configuration.GetSection("Appsettings:GeminiAPIKEY").Value!;
            string model = _configuration.GetSection("Appsettings:GeminiModel").Value!;

            GenerativeModel generativeModel = new GenerativeModel(apiKey, model);
            GenerateContentResponse aiResponse = await generativeModel.GenerateContentAsync(prompt);
            string response = aiResponse.Text.Trim();

            return DeserializeInterviews<T>(response);
        }

        private List<Interview> DeserializeInterviews<T>(string response) where T : class
        {
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            if (typeof(T) == typeof(HRInterview))
                return JsonSerializer.Deserialize<List<HRInterview>>(response, options)?.Cast<Interview>().ToList() ?? new List<Interview>();
    
            if (typeof(T) == typeof(TechnicalInterview))
                return JsonSerializer.Deserialize<List<TechnicalInterview>>(response, options)?.Cast<Interview>().ToList() ?? new List<Interview>();

            return [];
        }
    }
}