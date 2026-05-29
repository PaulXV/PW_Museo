using Models;

namespace PW_Museo.Client.Services.Visits;
{
    public interface IVisitService
    {
        Task<bool> CreateVisit(Visit visit);
        Task<bool> DeleteVisit(Guid id);
        Task<Visit[]> GetAllVisits();
        Task<Visit?> GetVisitById(Guid id);
        Task<bool> UpdateVisit(Visit visit);
    }
}