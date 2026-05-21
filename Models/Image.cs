namespace Models;

public class Image
{
    public required Guid Id { get; set; }
    public required string Src { get; set; } = string.Empty;
}
