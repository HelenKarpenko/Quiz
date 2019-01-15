using System;
using System.Linq;
using System.Linq.Expressions;

namespace Quiz.DAL.Interfaces
{
	public interface IRepository<T> where T : class
	{
		IQueryable<T> GetAll();
		
		T Get(int id);

		IQueryable<T> Find(Expression<Func<T, bool>> predicate);
		
		T Create(T item);

		T Update(int id, T item);

		T Delete(int id);
	}
}
