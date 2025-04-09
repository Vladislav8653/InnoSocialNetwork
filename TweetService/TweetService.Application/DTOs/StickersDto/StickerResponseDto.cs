namespace TweetService.Application.DTOs.StickersDto;

public record StickerResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}