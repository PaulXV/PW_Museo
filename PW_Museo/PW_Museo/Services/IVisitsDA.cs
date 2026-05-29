using Models;

namespace PW_Museo.Services
{
    public interface IVisitsDA
    {
        Task CreateAsync(Visit visit);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Visit>> GetAllAsync();
        Task<Visit?> GetByIdAsync(Guid id);
        Task UpdateAsync(Visit visit);
    }
}