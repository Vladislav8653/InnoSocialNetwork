namespace UserService.Application.DTO.PasswordResetDto;

public record EmailForPasswordResetDto
{
    public string Email { get; init; }
}