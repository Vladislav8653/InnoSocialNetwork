using AutoMapper;
using NotificationsService.Application.DTOs.NotificationsDto;
using NotificationsService.Domain.Models;

namespace NotificationsService.Application.MappingProfiles;

public class NotificationsMappingProfile : Profile
{
    public NotificationsMappingProfile()
    {
        CreateMap<Notification, NotificationRequestDto>();
    }
}