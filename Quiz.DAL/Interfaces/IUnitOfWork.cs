using Quiz.DAL.Entities;
using Quiz.DAL.Entities.UserResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.DAL.Interfaces
{
  public interface IUnitOfWork : IDisposable
  {
    IRepository<Test> Tests { get; }
    IRepository<Question> Questions { get; }
    IRepository<Answer> Answers { get; }
    IRepository<ResultDetails> ResultDetails { get; }
    IRepository<TestResult> TestResults { get; }

    //ApplicationUserManager UserManager { get; }
    //ApplicationRoleManager RoleManager { get; }

    void Save();
    Task SaveAsync();
  }
}
