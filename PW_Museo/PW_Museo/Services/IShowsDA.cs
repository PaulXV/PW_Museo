using Models;

namespace PW_Museo.Services
{
    public interface IShowsDA
    {
        Task CreateAsync(Show show);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Show>> GetAllAsync();
        Task<Show> GetByIdAsync(Guid id);
        Task UpdateAsync(Show show);
    }
}