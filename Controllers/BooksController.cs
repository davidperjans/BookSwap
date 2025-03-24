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
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
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

        [HttpPost("add")]
        public async Task<IActionResult> AddBook([FromBody] BookDto bookDto)
        {
            var senderId = GetSenderIdFromToken();
            if (senderId.HasValue)
            {
                var result = await _bookService.AddBookAsync(senderId.Value, bookDto);
                if (!result.IsSuccess)
                {
                    return BadRequest(result.ErrorMessage);
                }
                return Ok(result.Message);
            }
            return Unauthorized("Invalid user ID.");
        }

        [HttpDelete("delete/{bookId}")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var senderId = GetSenderIdFromToken();
            if (senderId.HasValue)
            {
                var result = await _bookService.DeleteBookAsync(senderId.Value, bookId);
                if (!result.IsSuccess)
                {
                    return BadRequest(result.ErrorMessage);
                }
                return Ok(result.Message);
            }
            return Unauthorized("Invalid user ID.");
        }

        [HttpPut("change-owner/{bookId}")]
        public async Task<IActionResult> ChangeBookOwner(int bookId, [FromBody] int newOwnerId)
        {
            var result = await _bookService.ChangeBookOwnerAsync(bookId, newOwnerId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Message);
        }

        [HttpGet("user-books")]
        public async Task<IActionResult> GetBooksByUser()
        {
            var senderId = GetSenderIdFromToken();
            if (senderId.HasValue)
            {
                var books = await _bookService.GetBooksByUserAsync(senderId.Value);
                return Ok(books);
            }
            return Unauthorized("Invalid user ID.");
        }
    }
}
