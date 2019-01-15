using Quiz.DAL.Context;
using Quiz.DAL.Entities.UserResults;
using Quiz.DAL.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Quiz.DAL.Repositories
{
	public class ResultDetailsRepository : IRepository<ResultDetails>
	{
		private EFContext _db;

		public ResultDetailsRepository(EFContext context)
		{
			_db = context ?? throw new ArgumentNullException("Сontext must not be null.");
		}

		public ResultDetails Create(ResultDetails item)
		{
			if (item == null)
				throw new ArgumentNullException("Result details must not be null.");

			return _db.ResultDetails.Add(item);
		}

		public ResultDetails Delete(int id)
		{
			if (id <= 0)
				throw new ArgumentException("Incorrect result details id.");

			ResultDetails details = _db.ResultDetails.Find(id);

			if (details == null) return null;

			return _db.ResultDetails.Remove(details);
		}

		public IQueryable<ResultDetails> Find(Expression<Func<ResultDetails, bool>> predicate)
		{
			if (predicate == null)
				throw new ArgumentNullException("Result details must not be null.");

			return _db.ResultDetails.Where(predicate);
		}

		public ResultDetails Get(int id)
		{
			if (id <= 0)
				throw new ArgumentException("Incorrect result details id.");

			return _db.ResultDetails.Find(id);
		}

		public IQueryable<ResultDetails> GetAll()
		{
			return _db.ResultDetails;
		}
		
		public ResultDetails Update(int id, ResultDetails item)
		{
			if (item == null)
				throw new ArgumentNullException("Item must not be null.");
			if (id <= 0)
				throw new ArgumentException("Incorrect result details id.");

			ResultDetails details = _db.ResultDetails.FirstOrDefault(x => x.Id == id);
			if (details == null) return null;

			_db.Entry(details).CurrentValues.SetValues(item);
			return details;
		}
	}
}
