using Models;

namespace PW_Museo.Services
{
    public interface IArtistsDA
    {
        Task CreateAsync(Artist artist);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Artist>> GetAllAsync();
        Task<Artist?> GetByIdAsync(Guid id);
        Task UpdateAsync(Artist artist);
        Task<IEnumerable<Artist>> GetAllWithOperaAsync();
    }
}