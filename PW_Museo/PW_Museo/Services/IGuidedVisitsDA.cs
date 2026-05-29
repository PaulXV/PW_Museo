using Models;

namespace PW_Museo.Services
{
    public interface IGuidedVisitsDA
    {
        Task CreateAsync(Guided_Visit guidedVisit);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Guided_Visit>> GetAllAsync();
        Task<Guided_Visit?> GetByIdAsync(Guid id);
        Task UpdateAsync(Guided_Visit guidedVisit);
    }
}