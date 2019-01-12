using Microsoft.AspNet.Identity.EntityFramework;
using Quiz.DAL.Context;
using Quiz.DAL.Entities;
using Quiz.DAL.Entities.UserResults;
using Quiz.DAL.Identity;
using Quiz.DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace Quiz.DAL.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private EFContext db;

		private TestRepository testRepository;
		private QuestionRepository questionRepository;
		private AnswerRepository answerRepository;
		private TestResultRepository testResultRepository;
		private ResultDetailsRepository resultDetailsRepository;

		private ApplicationUserManager userManager;
		private ApplicationRoleManager roleManager;

		public UnitOfWork(string connectionString)
		{
			if (connectionString == null)
				throw new ArgumentNullException("ConnectionString must not be null.");

			db = new EFContext(connectionString);
		}

		public IRepository<Test> Tests
		{
			get
			{
				if (testRepository == null)
					testRepository = new TestRepository(db);

				return testRepository;
			}
		}

		public IRepository<Question> Questions
		{
			get
			{
				if (questionRepository == null)
					questionRepository = new QuestionRepository(db);

				return questionRepository;
			}
		}

		public IRepository<Answer> Answers
		{
			get
			{
				if (answerRepository == null)
					answerRepository = new AnswerRepository(db);

				return answerRepository;
			}
		}

		public IRepository<TestResult> TestResults
		{
			get
			{
				if (testResultRepository == null)
					testResultRepository = new TestResultRepository(db);

				return testResultRepository;
			}
		}

		public IRepository<ResultDetails> ResultDetails
		{
			get
			{
				if (resultDetailsRepository == null)
					resultDetailsRepository = new ResultDetailsRepository(db);

				return resultDetailsRepository;
			}
		}

		public ApplicationUserManager UserManager
		{
			get
			{
				if (userManager == null)
					userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));

				return userManager;
			}
		}

		public ApplicationRoleManager RoleManager
		{
			get
			{
				if (roleManager == null)
					roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));

				return roleManager;
			}
		}

		public void Save()
		{
			db.SaveChanges();
		}

		public async Task SaveAsync()
		{
			await db.SaveChangesAsync();
		}

		#region Dispose

		private bool disposed = false;

		public virtual void Dispose(bool disposed)
		{
			if (!this.disposed)
			{
				if (disposed)
				{
					db.Dispose();
				}
				this.disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

	}
}
