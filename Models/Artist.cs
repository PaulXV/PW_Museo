namespace Models;

public class Artist
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
    public Guid OperaId { get; set; }
}