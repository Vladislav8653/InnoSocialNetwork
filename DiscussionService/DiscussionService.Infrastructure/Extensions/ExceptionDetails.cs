namespace DiscussionService.Infrastructure.Extensions;

public class ExceptionDetails
{
    public ExceptionDetails()
    {
        Type = Message = string.Empty;
    }

    public string Type { get; set; }
    public string Message { get; set; }
    
}