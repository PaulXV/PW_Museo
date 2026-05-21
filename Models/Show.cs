using System.ComponentModel.DataAnnotations;

namespace Models;

public class Show
{
    public required Guid Id { get; set; }
    public required string Title { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public required Guid ImageId { get; set; }
    public required Guid AuthorId { get; set; }
}
