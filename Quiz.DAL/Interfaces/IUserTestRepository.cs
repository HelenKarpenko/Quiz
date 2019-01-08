using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DAL.Interfaces
{
  public interface IUserTestRepository<T> where T : class
  {
    T Create(T userTest);

    T Delete(string userId, int testId, DateTime passageDate);

    IEnumerable<T> Find(Func<T, bool> predicate);

    T Get(string userId, int testId, DateTime passageDate);

    IEnumerable<T> GetAll();
  }
}
