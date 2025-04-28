using AutoMapper;
using TweetService.Application.DTOs.StickersDto;
using TweetService.Domain.Models;

namespace TweetService.Application.MappingProfiles;

public class StickerMappingProfile : Profile
{
    public StickerMappingProfile()
    {
        CreateMap<StickerRequestDto, Sticker>();
        CreateMap<Sticker, StickerResponseDto>();
    }
}