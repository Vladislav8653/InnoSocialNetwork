namespace TweetService.Application.DTOs.StickersDto;

public record StickerResponseDto
{
    public Guid Id { get; init; }
    public string Text { get; init; }
}