using Models;

namespace PW_Museo.Client.Services.Operas
{
    public interface IOperaService
    {
        Task<bool> CreateOpera(Opera newOpera);
        Task<bool> DeleteOpera(Guid id);
        Task<Opera[]> GetAllOperas();
        Task<Opera?> GetOperaById(Guid id);
        Task<bool> UpdateOpera(Opera updatedOpera);
    }
}