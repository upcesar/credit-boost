using CreditTopUp.Domain.Entities;
using System.Linq.Expressions;

namespace CreditTopUp.Domain.Interfaces;

public interface IRepository<T> : IReadonlyRepository<T>, IWriteOnlyRepository<T> where T : Entity { }

public interface IWriteOnlyRepository<T> where T : Entity
{
    void Add(T entity);
    void Add(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(T entity);
}

public interface IReadonlyRepository<T> where T : Entity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task<T> GetByAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetManyByAsync(Expression<Func<T, bool>> predicate);
}
