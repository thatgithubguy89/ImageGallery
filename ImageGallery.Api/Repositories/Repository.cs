using AutoMapper;
using ImageGallery.Api.Data;
using ImageGallery.Api.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ImageGallery.Api.Repositories
{
    public class Repository<DbModel, DtoModel> : IRepository<DbModel, DtoModel> where DbModel : class where DtoModel : class
    {
        private readonly ImageGalleryDbContext _context;
        private readonly IMapper _mapper;

        public Repository(ImageGalleryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected List<DtoModel> Convert(List<DbModel> entities)
        {
            return _mapper.Map<List<DtoModel>>(entities);
        }

        protected DtoModel Convert(DbModel entity)
        {
            return _mapper.Map<DtoModel>(entity);
        }

        protected DbModel Convert(DtoModel entity)
        {
            return _mapper.Map<DbModel>(entity);
        }

        public virtual async Task<DtoModel> AddAsync(DtoModel entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var converted = Convert(entity);

            var newEntity = await _context.Set<DbModel>().AddAsync(converted);
            await _context.SaveChangesAsync();

            return Convert(newEntity.Entity);
        }

        public virtual async Task DeleteAsync(DtoModel entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var converted = Convert(entity);

            _context.Set<DbModel>().Remove(converted);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<List<DtoModel>> GetAllAsync()
        {
            var entities = await _context.Set<DbModel>().ToListAsync();

            return Convert(entities);
        }

        public virtual async Task<DtoModel> GetByIdAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(id, 0);

            var entity = await _context.Set<DbModel>().FindAsync(id);

            if (entity == null)
                return null!;

            _context.Entry(entity).State = EntityState.Detached;

            return Convert(entity);
        }

        public virtual async Task UpdateAsync(DtoModel entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var converted = Convert(entity);

            _context.Set<DbModel>().Update(converted);
            await _context.SaveChangesAsync();
        }
    }
}
