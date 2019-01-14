using Quiz.DAL.Context;
using Quiz.DAL.Entities.UserResults;
using Quiz.DAL.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Quiz.DAL.Repositories
{
  public class TestResultRepository : IRepository<TestResult>
  {
    private EFContext _db;

    public TestResultRepository(EFContext context)
    {
      _db = context ?? throw new ArgumentNullException("Сontext must not be null.");
    }
    
    public TestResult Create(TestResult result)
    {
      if (result == null)
        throw new ArgumentNullException("Test result must not be null.");

      return _db.TestResults.Add(result);
    }

    public TestResult Delete(int id)
    {
      if (id <= 0)
        throw new ArgumentException("Incorrect test result id.");

      TestResult result = _db.TestResults.Find(id);
      if (result == null) return null;

      return _db.TestResults.Remove(result);
    }

    public IEnumerable<TestResult> Find(Func<TestResult, bool> predicate)
    {
      if (predicate == null)
        throw new ArgumentNullException("Predicate must not be null.");

      return _db.TestResults.Where(predicate).ToList();
    }

    public async Task<IEnumerable<TestResult>> FindAsync(Func<TestResult, bool> predicate)
    {
      if (predicate == null)
        throw new ArgumentNullException("Predicate must not be null.");

      IEnumerable<TestResult> results = await _db.TestResults.ToListAsync();

      return results.Where(predicate).ToList();
    }

    public async Task<TestResult> GetAsync(int id)
    {
      if (id <= 0)
        throw new ArgumentException("Incorrect test result id.");

      return await _db.TestResults.FindAsync(id);
    }

    public TestResult Get(int id)
    {
      if (id <= 0)
        throw new ArgumentException("Incorrect test result id.");

      return _db.TestResults.Find(id);
    }

    public IEnumerable<TestResult> GetAll()
    {
      return _db.TestResults.ToList();
    }

    public async Task<IEnumerable<TestResult>> GetAllAsync()
    {
      return await _db.TestResults
				
				.ToListAsync();
    }
    
    public TestResult Update(int id, TestResult item)
    {
      if (item == null)
        throw new ArgumentNullException("Item must not be null.");
      if (id <= 0)
        throw new ArgumentException("Incorrect test result id.");

      TestResult result = _db.TestResults.FirstOrDefault(x => x.Id == item.Id);
      if (result == null) return null;

      _db.Entry(result).CurrentValues.SetValues(item);
      return result;
    }
  }
}
