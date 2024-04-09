namespace ImageGallery.Api.Interfaces.Repositories
{
    public interface IRepository<DbModel, DtoModel>
    {
        Task<DtoModel> AddAsync(DtoModel entity);
        Task DeleteAsync(DtoModel entity);
        Task<List<DtoModel>> GetAllAsync();
        Task<DtoModel> GetByIdAsync(int id);
        Task UpdateAsync(DtoModel entity);
    }
}
