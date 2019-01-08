using Quiz.DAL.Context;
using Quiz.DAL.Entities;
using Quiz.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Quiz.DAL.Repositories
{
  public class AnswerRepository : IRepository<Answer>
  {
    private EFContext db;

    public AnswerRepository(EFContext context)
    {
      db = context ?? throw new ArgumentNullException("Сontext must not be null.");
    }

    public Answer Create(Answer answer)
    {
      if (answer == null)
        throw new ArgumentNullException("Answer must not be null");

      return db.Answers.Add(answer);
    }

    public Answer Delete(int id)
    {
      if (id <= 0)
        throw new ArgumentException("Incorrect answer id.");

      Answer answer = db.Answers.Find(id);

      if (answer == null) return null;

      return db.Answers.Remove(answer);
    }

    public async Task<IEnumerable<Answer>> FindAsync(Func<Answer, bool> predicate)
    {
      if (predicate == null)
        throw new ArgumentNullException("Predicate must not be null.");

      IEnumerable<Answer> answers = await db.Answers.ToListAsync();

      return answers.Where(predicate).ToList();
    }

    public IEnumerable<Answer> Find(Func<Answer, bool> predicate)
    {
      if (predicate == null)
        throw new ArgumentNullException("Predicate must not be null.");

      return db.Answers.Where(predicate).ToList();
    }

    public async Task<Answer> GetAsync(int id)
    {
      if (id <= 0)
        throw new ArgumentException("incorrect answer id.");

      return await db.Answers.FindAsync(id);
    }

    public Answer Get(int id)
    {
      if (id <= 0)
        throw new ArgumentException("incorrect answer id.");

      return db.Answers.Find(id);
    }

    public async Task<IEnumerable<Answer>> GetAllAsync()
    {
      return await db.Answers.ToListAsync();
    }

    public IEnumerable<Answer> GetAll()
    {
      return db.Answers.ToList();
    }

    public Answer Update(int id, Answer item)
    {
      if (item == null)
        throw new ArgumentNullException("Item must not be null.");

      if (id <= 0)
        throw new ArgumentException("incorrect answer id ");

      Answer answer = db.Answers.FirstOrDefault(x => x.Id == item.Id);
      if (answer == null) return null;

      db.Entry(answer).CurrentValues.SetValues(item);

      return answer;
    }
  }
}
