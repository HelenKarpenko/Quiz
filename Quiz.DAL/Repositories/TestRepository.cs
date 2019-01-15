using Quiz.DAL.Context;
using Quiz.DAL.Entities;
using Quiz.DAL.Interfaces;
using System;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Quiz.DAL.Repositories
{
	public class TestRepository : IRepository<Test>
	{
		private EFContext db;

		public TestRepository(EFContext context)
		{
			db = context ?? throw new ArgumentNullException("Сontext must not be null.");
		}

		public Test Create(Test test)
		{
			if (test == null)
				throw new ArgumentNullException("Test must not be null.");

			return db.Tests.Add(test);
		}

		public Test Delete(int id)
		{
			if (id <= 0)
				throw new ArgumentException("Incorrect test id.");

			Test test = db.Tests.Find(id);

			if (test == null) return null;

			return db.Tests.Remove(test);
		}

		public IQueryable<Test> Find(Expression<Func<Test, bool>> predicate)
		{
			if (predicate == null)
				throw new ArgumentNullException("Predicate must not be null.");

			return db.Tests.Where(predicate);
		}
		
		public Test Get(int id)
		{
			if (id <= 0)
				throw new ArgumentException("Incorrect test id.");

			return db.Tests.Find(id);
		}
		
		public IQueryable<Test> GetAll()
		{
			return db.Tests;
		}
		
		public Test Update(int id, Test item)
		{
			if (item == null)
				throw new ArgumentNullException("Item must not be null.");

			if (id <= 0)
				throw new ArgumentException("Incorrect test id.");

			Test test = db.Tests.FirstOrDefault(x => x.Id == id);
			if (test == null) return null;

			db.Entry(test).CurrentValues.SetValues(item);

			return test;
		}
	}
}
