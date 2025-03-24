using BookSwap.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookSwap.DTOs;
using System.Security.Claims;

namespace BookSwap.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TradeRequestsController : ControllerBase
    {
        private readonly ITradeRequestService _tradeRequestService;

        public TradeRequestsController(ITradeRequestService tradeRequestService)
        {
            _tradeRequestService = tradeRequestService;
        }

        private int? GetIdFromToken()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdString, out int userId))
            {
                return userId;
            }
            return null;
        }

        [HttpPost("send-traderequest")]
        public async Task<IActionResult> SendTradeRequest([FromBody] TradeRequestDto tradeRequestDto)
        {
            var senderId = GetIdFromToken();

            if (senderId.HasValue)
            {
                var result = await _tradeRequestService.SendTradeRequestAsync(senderId.Value, tradeRequestDto.ReceiverId, tradeRequestDto.SenderBookId, tradeRequestDto.ReceiverBookId);
                if (!result.IsSuccess)
                {
                    return BadRequest(result.ErrorMessage);
                }
                return Ok(result);
            }
            return Unauthorized("Invalid user ID.");
        }

        [HttpPost("accept/{requestId}")]
        public async Task<IActionResult> AcceptTradeRequest(int requestId)
        {
            var recieverId = GetIdFromToken();

            if (recieverId.HasValue)
            {
                var result = await _tradeRequestService.AcceptTradeRequestAsync(requestId, recieverId.Value);
                if (!result.IsSuccess)
                {
                    return BadRequest(result.ErrorMessage);
                }
                return Ok(result);
            }
            return Unauthorized("Invalid user ID.");
        }

        [HttpPost("reject/{requestId}")]
        public async Task<IActionResult> RejectTradeRequest(int requestId)
        {
            var recieverId = GetIdFromToken();

            if (recieverId.HasValue)
            {
                var result = await _tradeRequestService.RejectTradeRequestAsync(requestId, recieverId.Value);
                if (!result.IsSuccess)
                {
                    return BadRequest(result.ErrorMessage);
                }
                return Ok(result);
            }
            return Unauthorized("Invalid user ID.");
        }

        [HttpGet("pending/{userId}")]
        public async Task<IActionResult> GetPendingTradeRequests(int userId)
        {
            var requests = await _tradeRequestService.GetPendingTradeRequestsByUserAsync(userId);
            return Ok(requests);
        }
    }
}
