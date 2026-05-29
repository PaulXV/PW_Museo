using Models;

namespace PW_Museo.Client.Services.Shows
{
    public interface IShowService
    {
        Task<bool> CreateShow(Show newShow);
        Task<bool> DeleteShow(Guid id);
        Task<Show[]> GetAllShows();
        Task<Show?> GetShowById(Guid id);
        Task<bool> UpdateShow(Show updatedShow);
    }
}