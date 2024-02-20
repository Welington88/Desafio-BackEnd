namespace BackEnd.Domain.SeedWork;

public interface IRepository
{
    Task<string> AddObject<T>(T ObjectInsert);

    Task<string> UpdateObject<T>(T ObjectUpdate);

    Task<string> DeleteObject<T>(T ObjectRemove);
}

