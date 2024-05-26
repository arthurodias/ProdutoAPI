namespace ProductManagement.Domain.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAsync();
        Task<IEnumerable<T>> GetByFilterAsync(string descricao, string situacao, int pageNumber, int pageSize);
        Task InsertAsync(T dto);
        Task UpdateAsync(T dto);
        Task DeleteAsync(int id);
    }
}