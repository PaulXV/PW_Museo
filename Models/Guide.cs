using System.ComponentModel.DataAnnotations;

namespace Models;

public class Guide : User
{
    public required Guid VisitId { get; set; }
}
