using Models;

namespace PW_Museo.Services
{
    public interface ITicketsDA
    {
        Task CreateAsync(Ticket ticket);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<Ticket?> GetByIdAsync(Guid id);
        Task UpdateAsync(Ticket ticket);
    }
}