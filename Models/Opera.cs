using System.ComponentModel.DataAnnotations;

namespace Models;

public class Opera
{
    public required Guid Id { get; set; }
    public required string Title { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
    public required DateTime creationYear { get; set; }
    public required string Techinic { get; set; } = string.Empty;
    public required string Typology { get; set; } = string.Empty;
    public Guid ShowId { get; set; }
    public required Guid ImageId { get; set; }
    public required Guid AuthorId { get; set; }
}
