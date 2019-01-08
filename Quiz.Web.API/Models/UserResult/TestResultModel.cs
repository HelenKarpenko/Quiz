using System;
using System.Collections.Generic;

namespace Quiz.Web.API.Models.UserResult
{
  public class TestResultModel
  {
    public int Id { get; set; }

    public int TestId { get; set; }

    public string UserId { get; set; }

    public DateTime PassageDate { get; set; }

    //public virtual ICollection<ResultDetailsModel> Details { get; set; }

    public Dictionary<int, int> Answers { get; set; }

    public TestResultModel()
    {
      //Details = new HashSet<ResultDetailsModel>();
      Answers = new Dictionary<int, int>();
    }
  }
}
