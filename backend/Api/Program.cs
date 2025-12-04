using Application.UseCases;
using Domain.Interfaces;
using Domain.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS for frontend (including Docker origins)
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy
		.AllowAnyOrigin()      // allow all origins
		.AllowAnyHeader()      // allow all headers
		.AllowAnyMethod()      // allow all HTTP methods
		.WithExposedHeaders("*"); // for exposing headers
	});
});

// Register DDD layers
// Infrastructure (implements interfaces)
builder.Services.AddSingleton<IConversationRepository, InMemoryConversationRepository>();
builder.Services.AddScoped<IChatbotService, RuleBasedChatbotService>();

// Application (use cases)
builder.Services.AddScoped<SendMessageUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();
