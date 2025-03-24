using BookSwap.DTOs;

namespace BookSwap.Services
{
    public interface IUserService
    {
        Task<(bool IsSuccess, string ErrorMessage)> RegisterAsync (RegisterDto registerDto);
        Task<(bool IsSuccess, string Token, string ErrorMessage)> LoginAsync(LoginDto loginDto);
    }
}
