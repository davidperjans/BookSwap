using AutoMapper;
using BookSwap.DTOs;
using BookSwap.Models;

namespace BookSwap.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<LoginDto, User>();
            CreateMap<FriendRequestDto, FriendRequest>();
            CreateMap<TradeRequestDto, TradeRequest>();
            CreateMap<BookDto,  Book>();
        }
    }
}
