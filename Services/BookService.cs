using BookSwap.Data;
using BookSwap.DTOs;
using BookSwap.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSwap.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult> AddBookAsync(int userId, BookDto bookDto)
        {
            var serviceResult = new ServiceResult();

            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Genre = bookDto.Genre,
                Description = bookDto.Description,
                OwnerId = userId
            };
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            serviceResult.IsSuccess = true;
            serviceResult.Message = "Book added successfully";

            return serviceResult;
        }

        public async Task<ServiceResult> DeleteBookAsync(int userId, int bookId)
        {
            var serviceResult = new ServiceResult();

            var book = await _context.Books.FirstOrDefaultAsync(book => book.Id == bookId && book.OwnerId == userId);

            if (book == null)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = "Book not found or you are not the owner of the book";
                return serviceResult;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            serviceResult.IsSuccess = true;
            serviceResult.Message = "Book deleted successfully";

            return serviceResult;
        }

        public async Task<ServiceResult> ChangeBookOwnerAsync(int bookId, int newOwnerId)
        {
            var serviceResult = new ServiceResult();

            var book = await _context.Books.FirstOrDefaultAsync(book => book.Id == bookId);
            if (book == null)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = "Book not found";
                return serviceResult;
            }

            book.OwnerId = newOwnerId;
            await _context.SaveChangesAsync();

            serviceResult.IsSuccess = true;
            serviceResult.Message = "Book owner changed successfully";
            return serviceResult;
        }

        public async Task<IEnumerable<Book>> GetBooksByUserAsync(int userId)
        {
            return await _context.Books.Where(book => book.OwnerId == userId).ToListAsync();
        }
    }
}
