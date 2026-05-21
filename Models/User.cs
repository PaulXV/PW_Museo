using System.ComponentModel.DataAnnotations;

namespace Models;

public class User
{
    public required Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string Surname { get; set; } = string.Empty;
    public required EmailAddressAttribute EmailAddress { get; set; }
    public required bool IsAdmin { get; set; } = false;
}
