namespace UserService.Application.DTO.HideUserDto;

public record HideUserDto
{
    public string UserId { get; init; }
    public bool Hide { get; init; }
}