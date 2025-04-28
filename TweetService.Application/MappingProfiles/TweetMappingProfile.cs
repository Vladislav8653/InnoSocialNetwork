using AutoMapper;
using TweetService.Application.DTOs.TweetsDto;
using TweetService.Domain.Models;

namespace TweetService.Application.MappingProfiles;

public class TweetMappingProfile : Profile
{
    public TweetMappingProfile()
    {
        CreateMap<TweetRequestDto, Tweet>();
        CreateMap<Tweet, TweetResponseDto>();
    }
}