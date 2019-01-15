using Quiz.DAL.Context;
using Quiz.DAL.Entities;
using Quiz.DAL.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Quiz.DAL.Repositories
{
	public class QuestionRepository : IRepository<Question>
	{
		private EFContext db;

		public QuestionRepository(EFContext context)
		{
			db = context ?? throw new ArgumentNullException("Сontext must not be null.");
		}

		public Question Create(Question question)
		{
			if (question == null)
				throw new ArgumentNullException("Question must not be null.");

			return db.Questions.Add(question);
		}

		public Question Delete(int id)
		{
			if (id <= 0)
				throw new ArgumentException("Incorrect question id.");

			Question question = db.Questions.Find(id);

			if (question == null) return null;

			return db.Questions.Remove(question);
		}

		public IQueryable<Question> Find(Expression<Func<Question, bool>> predicate)
		{
			if (predicate == null)
				throw new ArgumentNullException("Predicate must not be null.");

			return db.Questions.Where(predicate);
		}
		
		public Question Get(int id)
		{
			if (id <= 0)
				throw new ArgumentException("Incorrect question id.");

			return db.Questions.Find(id);
		}
		
		public IQueryable<Question> GetAll()
		{
			return db.Questions;
		}
		
		public Question Update(int id, Question item)
		{
			if (item == null)
				throw new ArgumentNullException("Item must not be null.");

			if (id <= 0)
				throw new ArgumentException("Incorrect item id.");

			Question question = db.Questions.FirstOrDefault(x => x.Id == id);
			if (question == null) return null;

			db.Entry(question).CurrentValues.SetValues(item);

			return question;
		}
	}
}
