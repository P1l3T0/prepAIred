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

        private async Task<List<Interview>> AskChatGPTAsync(string prompt)
        {
            string apiKey = _configuration.GetSection("Appsettings:OpenAIAPIKEY").Value!;
            string model = _configuration.GetSection("Appsettings:OpenAIModel").Value!;

            ChatClient chatClient = new ChatClient(model, apiKey);
            ClientResult<ChatCompletion> completion = await chatClient.CompleteChatAsync(prompt);
            string response = completion.Value.Content[0].Text.Trim();

            return DeserializeInterviews(response);
        }

        private async Task<List<Interview>> AskClaudeAsync(string prompt)
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

            return DeserializeInterviews(response);
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

            return interviews;
        }
    }
}