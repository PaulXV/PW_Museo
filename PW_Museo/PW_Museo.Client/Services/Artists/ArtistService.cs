using Models;
using System.Net.Http.Json;

namespace PW_Museo.Client.Services.Artists;

public class ArtistService : IArtistService
{
    private readonly HttpClient Http;

    public ArtistService(HttpClient http)
    {
        Http = http;
    }

    public async Task<Artist[]> GetAllArtists()
    {
        try
        {
            var response = await Http.GetAsync("api/operas");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<Artist[]>().Result ?? [];
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching artits: {ex.Message}");
            return [];
        }
    }

    public async Task<Artist?> GetArtistById(Guid id)
    {
        try
        {
            var response = await Http.GetAsync($"api/artists/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Artist>();
            }
            else
            {
                Console.WriteLine($"Artist with ID {id} not found.");
                return null;
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching artist with ID {id}: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> CreateArtist(Artist newArtist)
    {
        try
        {
            var response = await Http.PostAsJsonAsync("api/artists", newArtist);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error creating artist: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateArtist(Artist updatedArtist)
    {
        try
        {
            var response = await Http.PutAsJsonAsync($"api/artists/{updatedArtist.Id}", updatedArtist);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error updating artist with ID {updatedArtist.Id}: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteArtist(Guid id)
    {
        try
        {
            var response = await Http.DeleteAsync($"api/artists/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error deleting artist with ID {id}: {ex.Message}");
            return false;
        }
    }
}
