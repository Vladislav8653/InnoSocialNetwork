using AutoMapper;
using TweetService.Application.DTOs.TweetsDto;
using TweetService.Domain.Models;

namespace TweetService.Application.MappingProfiles;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<TweetRequestToDto, Tweet>();
        CreateMap<Tweet, TweetResponseToDto>();
    }
}