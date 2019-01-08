using System;
using System.Collections.Generic;

namespace Quiz.DAL.Entities.UserResults
{
  public class TestResult
  {
    public int Id { get; set; }

    public int TestId { get; set; }
    public virtual Test Test { get; set; }

    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

    public DateTime PassageDate { get; set; }
    
    public virtual ICollection<ResultDetails> Details { get; set; }

    public TestResult()
    {
      Details = new HashSet<ResultDetails>();
    }
  }
}
