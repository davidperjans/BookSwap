using BookSwap.Data;
using BookSwap.DTOs;
using BookSwap.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookSwap.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FriendRequestsController : ControllerBase
    {
        private readonly IFriendRequestService _friendRequestService;

        public FriendRequestsController(IFriendRequestService friendRequestService)
        {
            _friendRequestService = friendRequestService;
        }


        private int? GetSenderIdFromToken()
        {
            var senderIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(senderIdString, out int senderId))
            {
                return senderId;
            }
            return null;
        }



        [HttpPost("send-friendrequest")]
        public async Task<IActionResult> SendFriendRequestAsync([FromBody] FriendRequestDto friendRequestDto)
        {
            var senderId = GetSenderIdFromToken();

            if (senderId.HasValue)
            {
                var result = await _friendRequestService.SendFriendRequestAsync(senderId.Value, friendRequestDto.ReceiverId);
                if (!result.IsSuccess)
                {
                    return BadRequest(result.ErrorMessage);
                }

                return Ok(result.Message);
            }

            return Unauthorized("Invalid user ID.");
        }

        [HttpPost("accept-friendrequest")]
        public async Task<IActionResult> AcceptFriendRequestAsync([FromBody] AcceptFriendRequestDto acceptFriendRequestDto)
        {
            var receiverId = GetSenderIdFromToken();

            if (receiverId.HasValue)
            {
                var result = await _friendRequestService.AcceptFriendRequestAsync(acceptFriendRequestDto.RequestId, receiverId.Value);
                if (!result.IsSuccess)
                {
                    return BadRequest(result.ErrorMessage);
                }
                return Ok(result.Message);
            }

            return Unauthorized("Invalid user ID.");
        }

        [HttpGet("pending/{userId}")]
        public async Task<IActionResult> GetPendingRequests(int userId)
        {
            var requests = await _friendRequestService.GetPendingFriendRequestsAsync(userId);
            return Ok(requests);
        }
    }
}
