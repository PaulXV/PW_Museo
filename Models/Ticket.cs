namespace Models;

public class Ticket
{
    public required Guid Id { get; set; }
    public required string Type { get; set; } = string.Empty;
    public required int Quantity { get; set; }
    public required int TotalPrice { get; set; }
    public required DateTime PurchaseDate { get; set; }
    public required Guid PersonId { get; set; }
    public required Guid ShowId { get; set; }
    public required Guid VisitId { get; set; }
}
