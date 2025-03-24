using AutoMapper;
using BookSwap.Services;
using BookSwap.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BookSwap.Data;
using System.Security.Claims;
using BookSwap.Validators;
using FluentValidation;

namespace BookSwap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        private readonly IValidator<LoginDto> _loginDtoValidator;
        private readonly IValidator<RegisterDto> _registerDtoValidator;

        public AuthController(IUserService userService, IMapper mapper, AppDbContext context, 
            IValidator<RegisterDto> registerDtoValidator, IValidator<LoginDto> loginDtoValidator)
        {
            _userService = userService;
            _mapper = mapper;
            _context = context;
            _registerDtoValidator = registerDtoValidator;
            _loginDtoValidator = loginDtoValidator;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
        {

            var validationResult = await _registerDtoValidator.ValidateAsync(registerDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(errorMessages);
            }


            var result = await _userService.RegisterAsync(registerDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok("User successfully registered");
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {

            var validationResult = await _loginDtoValidator.ValidateAsync(loginDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(errorMessages);
            }


            var result = await _userService.LoginAsync(loginDto);
            if (!result.IsSuccess)
            {
                return Unauthorized(result.ErrorMessage);
            }
            return Ok(new { Token = result.Token });
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.FirstOrDefault(user => user.Id.ToString() == userId);

            if (user == null)
            {
                return Unauthorized("User not found");
            }

            return Ok(user);
        }
    }
}
