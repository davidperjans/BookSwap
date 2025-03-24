using BookSwap.Models;
using BookSwap.DTOs;

namespace BookSwap.Services
{
    public interface IBookService
    {
        Task<ServiceResult> AddBookAsync(int userId, BookDto bookDto);
        Task<ServiceResult> DeleteBookAsync(int userId, int bookId);
        Task<ServiceResult> ChangeBookOwnerAsync(int bookId, int newOwnerId);
        Task<IEnumerable<Book>> GetBooksByUserAsync(int userId);
    }
}
