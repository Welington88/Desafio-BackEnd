namespace BackEnd.Domain.SeedWork;

public interface IRepositoryDapper
{
    Task<IEnumerable<T>> GetAll<T>(string query);

    Task<IEnumerable<T>> GetByIdList<T>(Guid Id, string query, object parameters);

    Task<T> GetById<T>(Guid Id, string query, object parameters);
}