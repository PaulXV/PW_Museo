namespace Models;

public class Visit
{
    public required Guid Id { get; set; }
    public required string Title { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
    public required DateTime DateTime { get; set; }
    public required int Duration { get; set; }
    public required int MaxParticipants { get; set; }
    public Guid ShowId { get; set; }
    public required Guid GuideId { get; set; }
    public required Guid AuthorId { get; set; }
}
