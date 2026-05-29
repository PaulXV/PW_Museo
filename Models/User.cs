using System.ComponentModel.DataAnnotations;

namespace Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public bool IsAdmin { get; set; } = false;
}
