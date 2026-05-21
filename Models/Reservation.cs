namespace Models;

public class Reservation
{
    public required Guid Id { get; set; }
    public required DateTime Date { get; set; }
    public bool Status { get; set; }
    public required Guid PersonId { get; set; }
    public required Guid VisitId { get; set; }
}
