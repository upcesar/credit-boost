using CreditBoost.Domain.Entities;

namespace CreditBoost.Domain.Interfaces;
public interface IRepository<T> where T : Entity
{

    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
