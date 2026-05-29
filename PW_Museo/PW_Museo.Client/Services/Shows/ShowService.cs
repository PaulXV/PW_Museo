using Models;
using System.Net.Http.Json;

namespace PW_Museo.Client.Services.Shows;

public class ShowService : IShowService
{
    private readonly HttpClient Http = new();

    public async Task<Show[]> GetAllShows()
    {
        try
        {
            var response = await Http.GetAsync("api/shows");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<Show[]>().Result ?? [];
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching shows: {ex.Message}");
            return [];
        }
    }

    public async Task<Show?> GetShowById(Guid id)
    {
        try
        {
            var response = await Http.GetAsync($"api/shows/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Show>();
            }
            else
            {
                Console.WriteLine($"Show with ID {id} not found.");
                return null;
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching show with ID {id}: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> CreateShow(Show newShow)
    {
        try
        {
            var response = await Http.PostAsJsonAsync("api/shows", newShow);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error creating show: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateShow(Show updatedShow)
    {
        try
        {
            var response = await Http.PutAsJsonAsync($"api/shows/{updatedShow.Id}", updatedShow);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error updating show with ID {updatedShow.Id}: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteShow(Guid id)
    {
        try
        {
            var response = await Http.DeleteAsync($"api/shows/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error deleting show with ID {id}: {ex.Message}");
            return false;
        }
    }
}
