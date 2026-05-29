using Models;

namespace PW_Museo.Services
{
    public interface IOperasDA
    {
        Task CreateAsync(Opera opera);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Opera>> GetAllAsync();
        Task<Opera> GetByIdAsync(Guid id);
        Task UpdateAsync(Opera opera);
    }
}