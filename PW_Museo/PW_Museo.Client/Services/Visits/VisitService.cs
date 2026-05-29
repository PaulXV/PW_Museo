using Models;
using System.Net.Http.Json;

namespace PW_Museo.Client.Services.Visits;

public class VisitService : IVisitService
{
    private readonly HttpClient Http;

    public VisitService(HttpClient http)
    {
        Http = http;
    }

    public async Task<Visit[]> GetAllVisits()
    {
        try
        {
            var response = await Http.GetAsync("api/visits");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<Visit[]>().Result ?? [];
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching visits: {ex.Message}");
            return [];
        }
    }

    public async Task<Visit?> GetVisitById(Guid id)
    {
        try
        {
            var response = await Http.GetAsync($"api/visits/{id}");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<Visit>().Result;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching visit with ID {id}: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> CreateVisit(Visit visit)
    {
        try
        {
            var response = await Http.PostAsJsonAsync("api/visits", visit);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error creating visit: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateVisit(Visit visit)
    {
        try
        {
            var response = await Http.PutAsJsonAsync($"api/visits/{visit.Id}", visit);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error updating visit with ID {visit.Id}: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteVisit(Guid id)
    {
        try
        {
            var response = await Http.DeleteAsync($"api/visits/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error deleting visit with ID {id}: {ex.Message}");
            return false;
        }
    }
}