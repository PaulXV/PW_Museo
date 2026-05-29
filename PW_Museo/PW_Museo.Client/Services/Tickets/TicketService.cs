using Models;
using System.Net.Http.Json;

namespace PW_Museo.Client.Services.Tickets;

public class TicketService : ITicketService
{
    private readonly HttpClient Http = new();

    public async Task<Ticket[]> GetAllTickets()
    {
        try
        {
            var response = await Http.GetAsync("api/tickets");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<Ticket[]>().Result ?? [];
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching tickets: {ex.Message}");
            return [];
        }
    }

    public async Task<Ticket?> GetTicketById(Guid id)
    {
        try
        {
            var response = await Http.GetAsync($"api/tickets/{id}");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<Ticket>().Result;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching ticket with ID {id}: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> CreateTicket(Ticket ticket)
    {
        try
        {
            var response = await Http.PostAsJsonAsync("api/tickets", ticket);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error creating ticket: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateTicket(Ticket ticket)
    {
        try
        {
            var response = await Http.PutAsJsonAsync($"api/tickets/{ticket.Id}", ticket);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error updating ticket with ID {ticket.Id}: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteTicket(Guid id)
    {
        try
        {
            var response = await Http.DeleteAsync($"api/tickets/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error deleting ticket with ID {id}: {ex.Message}");
            return false;
        }
    }
}
