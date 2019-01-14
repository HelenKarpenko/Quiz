using Quiz.DAL.Entities;
using Quiz.DAL.Entities.UserResults;
using System;
using System.Threading.Tasks;
using Quiz.DAL.Identity;
using Quiz.DAL.Entities.User;

namespace Quiz.DAL.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<Test> Tests { get; }
		IRepository<Question> Questions { get; }
		IRepository<Answer> Answers { get; }
		IRepository<ResultDetails> ResultDetails { get; }
		IRepository<TestResult> TestResults { get; }
		IRepository<UserInfo> UsersInfo { get; }

		ApplicationUserManager UserManager { get; }
		ApplicationRoleManager RoleManager { get; }

		void Save();
		Task SaveAsync();
	}
}
