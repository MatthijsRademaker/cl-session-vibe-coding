var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS for frontend
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:5174")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

// Simple chatbot responses
var responses = new[]
{
    "That's an interesting point! Tell me more.",
    "I understand. How does that make you feel?",
    "Fascinating! I'd love to hear your thoughts on that.",
    "That's a great question! Let me think about it...",
    "I see what you mean. Have you considered other perspectives?",
    "Thanks for sharing that with me!",
    "That's definitely something worth exploring further.",
    "Interesting perspective! What led you to that conclusion?",
    "I appreciate you telling me about this.",
    "That's quite thought-provoking!"
};

var greetings = new[] { "hello", "hi", "hey", "greetings", "howdy" };
var questions = new[] { "how are you", "what's up", "how's it going" };

app.MapPost("/api/chat", (ChatRequest request) =>
{
    var userMessage = request.Message.ToLower().Trim();
    string botResponse;

    // Simple rule-based responses
    if (greetings.Any(g => userMessage.Contains(g)))
    {
        botResponse = "Hello! I'm doing great, thanks for asking. How can I help you today?";
    }
    else if (questions.Any(q => userMessage.Contains(q)))
    {
        botResponse = "I'm doing wonderfully! Thanks for asking. How about you?";
    }
    else if (userMessage.Contains("bye") || userMessage.Contains("goodbye"))
    {
        botResponse = "Goodbye! It was nice chatting with you. Come back anytime!";
    }
    else if (userMessage.Contains("help"))
    {
        botResponse = "I'm here to chat with you! Ask me anything or just share what's on your mind.";
    }
    else if (userMessage.Contains("?"))
    {
        botResponse = "That's a great question! " + responses[Random.Shared.Next(responses.Length)];
    }
    else
    {
        botResponse = responses[Random.Shared.Next(responses.Length)];
    }

    return new ChatResponse(botResponse);
})
.WithName("Chat")
.WithOpenApi();

app.Run();

record ChatRequest(string Message);
record ChatResponse(string Response);
