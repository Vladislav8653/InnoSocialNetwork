using System.Text.Json.Serialization;

namespace UserService.Application.DTO;

public record UserRequestDto
{
    public enum UserRole
    {
        User = 1,
        Administrator = 2
    }
    
    public string UserName { get; init; }
    public string Email { get; init; }
    
    [JsonPropertyName("password")]
    public string PasswordHash { get; init; }
    public UserRole Role { get; init; }
}