using Quiz.DAL.Entities.UserResults;
using System.Collections.Generic;

namespace Quiz.DAL.Entities
{
  public class Question
  {
    public int Id { get; set; }

    public string Text { get; set; }

    public virtual ICollection<Answer> Answers { get; set; }

    public int TestId { get; set; }
    public virtual Test Test { get; set; }

    public virtual ICollection<ResultDetails> ResultDetails { get; set; }

    public Question()
    {
      Answers = new HashSet<Answer>();
      ResultDetails = new HashSet<ResultDetails>();
    }
  }
}
