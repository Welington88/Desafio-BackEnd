namespace BackEnd.Domain.SeedWork;

public interface IUnitOfWork
{
    IRepository Repository { get; }

    Task CommitAsync();
}