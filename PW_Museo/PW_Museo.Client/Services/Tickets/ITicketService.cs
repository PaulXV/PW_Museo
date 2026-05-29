using Models;

namespace PW_Museo.Client.Services.Tickets;

public interface ITicketService
{
    Task<bool> CreateTicket(Ticket ticket);
    Task<bool> DeleteTicket(Guid id);
    Task<Ticket[]> GetAllTickets();
    Task<Ticket?> GetTicketById(Guid id);
    Task<bool> UpdateTicket(Ticket ticket);
}