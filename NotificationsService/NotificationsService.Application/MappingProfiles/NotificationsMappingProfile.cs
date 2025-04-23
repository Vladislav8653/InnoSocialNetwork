using AutoMapper;
using NotificationsService.Application.DataTransferObjects.NotificationsDto;
using NotificationsService.Domain.Models;

namespace NotificationsService.Application.MappingProfiles;

public class NotificationsMappingProfile : Profile
{
    public NotificationsMappingProfile()
    {
        CreateMap<Notification, NotificationDto>();
    }
}