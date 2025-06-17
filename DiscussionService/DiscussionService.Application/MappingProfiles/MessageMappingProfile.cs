using AutoMapper;
using DiscussionService.Application.DTOs;
using DiscussionService.Domain.Models;

namespace DiscussionService.Application.MappingProfiles;

public class MessageMappingProfile : Profile
{
    public MessageMappingProfile()
    {
        CreateMap<MessageRequestDto, Message>();
        CreateMap<Message, MessageResponseDto>()
            .ForMember(m => m.Id, opt => 
                opt.MapFrom(src => src.Id.ToString()));
    }
}