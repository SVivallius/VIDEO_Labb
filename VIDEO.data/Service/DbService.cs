namespace VIDEO.Membership.data.Service;
public class DbService : IDbService
{
    private readonly IMapper _mapper;
    private readonly VideoDbContext _db;

    public DbService(IMapper mapper, VideoDbContext db)
    {
        _mapper = mapper;
        _db = db;
    }

    public async Task<bool> SaveChangesAsync()
    {
        int result = await _db.SaveChangesAsync();
        return result > 0;
    }

    async Task<bool> IDbService.AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
    {
        return await _db.Set<TEntity>().AnyAsync(expression);
    }

    async Task<TEntity> IDbService.CreateAsync<TEntity, TDto>(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _db.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    async Task<bool> IDbService.DeleteAsync<TEntity, TDto>(int id)
    {
        try
        {
            var entity = await GetByIdAsync<TEntity>(e => e.Id == id);
            if (entity == null) return false;
            
            _db.Remove(entity);
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    async Task<List<TDto>> IDbService.GetAllAsync<TEntity, TDto>()
    {
        var entities = await _db.Set<TEntity>().ToListAsync();
        return _mapper.Map<List<TDto>>(entities);
    }

    async Task<List<TDto>> IDbService.GetFilteredAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
    {
        var entities = await _db.Set<TEntity>()
            .Where(expression)
            .ToListAsync();
        return _mapper.Map<List<TDto>>(entities);
    }

    async Task<TDto> IDbService.GetByIdAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _db.Set<TEntity>().SingleOrDefaultAsync(expression);
        return _mapper.Map<TDto>(entity);
    }

    void IDbService.Update<TEntity, TDto>(int id, TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        entity.Id = id;

        _db.Update(entity);
    }

    // Supporting methods

    private async Task<IEntity?> GetByIdAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity
    {
        return await _db.Set<TEntity>().SingleOrDefaultAsync(expression);
    }

    void IDbService.Include<TEntity>()
    {
        var propertyNames =
            _db.Model.FindEntityType(typeof(TEntity))?.GetNavigations().Select(x => x.Name);

        if (propertyNames == null) return;

        foreach (var name in propertyNames) 
            _db.Set<TEntity>().Include(name).Load();
    }
}
