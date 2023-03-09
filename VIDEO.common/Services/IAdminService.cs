namespace VIDEO.common.Services
{
    public interface IAdminService
    {
        Task<List<TDto>> GetAsync<TDto>(string uri)
            where TDto : class;
    }
}