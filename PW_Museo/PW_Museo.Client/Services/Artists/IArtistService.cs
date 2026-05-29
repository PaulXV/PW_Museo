using Models;

namespace PW_Museo.Client.Services.Artists
{
    public interface IArtistService
    {
        Task<bool> CreateArtist(Artist newArtist);
        Task<bool> DeleteArtist(Guid id);
        Task<Artist[]> GetAllArtists();
        Task<Artist?> GetArtistById(Guid id);
        Task<bool> UpdateArtist(Artist updatedArtist);
    }
}