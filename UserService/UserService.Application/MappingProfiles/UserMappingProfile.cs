using AutoMapper;
using UserService.Application.DTO;
using UserService.Domain.Models;

namespace UserService.Application.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserRequestDto, User>();
    }
}
