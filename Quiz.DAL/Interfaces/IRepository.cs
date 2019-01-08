using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quiz.DAL.Interfaces
{
  public interface IRepository<T> where T : class
  {
    IEnumerable<T> GetAll();

    Task<IEnumerable<T>> GetAllAsync();

    T Get(int id);

    Task<T> GetAsync(int id);

    IEnumerable<T> Find(Func<T, bool> predicate);

    Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate);

    T Create(T item);

    T Update(int id, T item);

    T Delete(int id);
  }
}
