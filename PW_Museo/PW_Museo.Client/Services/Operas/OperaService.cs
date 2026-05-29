using Models;
using System.Net.Http.Json;

namespace PW_Museo.Client.Services.Operas;

public class OperaService : IOperaService
{
    private readonly HttpClient Http = new();

    public async Task<Opera[]> GetAllOperas()
    {
        try
        {
            var response = await Http.GetAsync("api/operas");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<Opera[]>().Result ?? [];
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching tickets: {ex.Message}");
            return [];
        }
    }

    public async Task<Opera?> GetOperaById(Guid id)
    {
        try
        {
            var response = await Http.GetAsync($"api/operas/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Opera>();
            }
            else
            {
                Console.WriteLine($"Opera with ID {id} not found.");
                return null;
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching opera with ID {id}: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> CreateOpera(Opera newOpera)
    {
        try
        {
            var response = await Http.PostAsJsonAsync("api/operas", newOpera);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error creating opera: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateOpera(Opera updatedOpera)
    {
        try
        {
            var response = await Http.PutAsJsonAsync($"api/operas/{updatedOpera.Id}", updatedOpera);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error updating opera with ID {updatedOpera.Id}: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteOpera(Guid id)
    {
        try
        {
            var response = await Http.DeleteAsync($"api/operas/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error deleting opera with ID {id}: {ex.Message}");
            return false;
        }
    }
}
