namespace TweetService.Application.DTOs.WritersDto;

public record WriterResponseDto
{
    public string Login { get; init; }
    public string FirstName { get; init; } 
    public string LastName { get; init; } 
}