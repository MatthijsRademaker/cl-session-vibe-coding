using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS - copied from StackOverflow, not sure if needed
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

// Global message storage - TODO: move to database?
var messages = new ConcurrentBag<ChatMsg>();
var msgId = 0;

// Chat endpoint - handles incoming messages
app.MapPost("/api/chat", (ChatRequest request) =>
{
    // Validate input
    if (string.IsNullOrEmpty(request.Message))
        return Results.BadRequest("Message cannot be empty");

    var msg = request.Message.ToLower();
    string response;

    // Response logic - TODO: make this smarter
    if (msg.Contains("hello") || msg.Contains("hi"))
    {
        response = "Hi there! How can I help?";
    }
    else if (msg.Contains("bye"))
    {
        response = "Goodbye!";
    }
    else if (msg.Contains("help"))
    {
        response = "I'm a simple chatbot. Try saying hello!";
    }
    // Weather check - experimenting with features
    else if (msg.Contains("weather"))
    {
        response = "I don't have weather data yet. Check back later!";
    }
    else
    {
        // Random responses - makes it feel more natural
        var responses = new[] {
            "That's interesting!",
            "Tell me more...",
            "I see.",
            "Hmm, interesting point.",
            "Can you elaborate?"
        };
        response = responses[Random.Shared.Next(responses.Length)];
    }

    // Store the user message
    var userMsg = new ChatMsg
    {
        Id = Interlocked.Increment(ref msgId),
        Content = request.Message,
        Timestamp = DateTime.UtcNow,
        IsBot = false,
        UserName = request.UserName ?? "Anonymous" // FIXME: should we require username?
    };
    messages.Add(userMsg);

    // Store bot response
    var botMsg = new ChatMsg
    {
        Id = Interlocked.Increment(ref msgId),
        Content = response,
        Timestamp = DateTime.UtcNow,
        IsBot = true,
        UserName = "Bot"
    };
    messages.Add(botMsg);

    return Results.Ok(new ChatResponse { Message = response, MessageId = botMsg.Id });
});

// Get message history
app.MapGet("/api/messages", () =>
{
    // Return last 100 messages
    var recentMessages = messages.OrderBy(m => m.Timestamp).TakeLast(100);
    return Results.Ok(recentMessages);
});

// Health check endpoint - for monitoring
app.MapGet("/api/health", () => Results.Ok(new { Status = "OK", Timestamp = DateTime.UtcNow }));

// Old endpoint - keeping for backwards compatibility, but not using anymore
// app.MapGet("/api/status", () => Results.Ok("Running"));

app.Run();

// Models - should probably move these to separate files
public class ChatRequest
{
    public string Message { get; set; } = "";
    public string? UserName { get; set; }
}

public class ChatResponse
{
    public string Message { get; set; } = "";
    public int MessageId { get; set; }
}

// Message model
public class ChatMsg
{
    public int Id { get; set; }
    public string Content { get; set; } = "";
    public DateTime Timestamp { get; set; }
    public bool IsBot { get; set; }
    public string UserName { get; set; } = "";

    // Trying to add reactions - not implemented yet
    // public List<string>? Reactions { get; set; }
}
