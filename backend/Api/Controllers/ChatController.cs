using Application.DTOs;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly SendMessageUseCase _sendMessageUseCase;

    public ChatController(SendMessageUseCase sendMessageUseCase)
    {
        _sendMessageUseCase = sendMessageUseCase;
    }

    [HttpPost]
    public async Task<ActionResult<ChatResponse>> SendMessage([FromBody] SendMessageRequest request)
    {
        try
        {
            var response = await _sendMessageUseCase.ExecuteAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred processing your message" });
        }
    }
}
