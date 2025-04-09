namespace TweetService.Application.DTOs.WritersDto;

public record WriterRequestDto
{
    public string Login { get; init; } 
    public string Password { get; init; } 
    public string FirstName { get; init; } 
    public string LastName { get; init; }
}