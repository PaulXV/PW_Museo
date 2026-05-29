using Models;

namespace PW_Museo.Services
{
    public interface IImagesDA
    {
        Task<Guid> CreateAsync(string src);
        Task DeleteAsync(Guid id);
        Task<Image?> GetByIdAsync(Guid id);
    }
}