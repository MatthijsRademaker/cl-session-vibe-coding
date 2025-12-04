using Application.DTOs;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/test")]
public class ChatControllerV2 : ControllerBase
{
	private readonly SendMessageUseCase _sendMessageUseCase;

	public ChatControllerV2(SendMessageUseCase sendMessageUseCase)
	{
		_sendMessageUseCase = sendMessageUseCase;
	}

	[HttpGet]
	public async Task<ActionResult> SendMessage()
	{
		return Ok("hello world");
	}
}
