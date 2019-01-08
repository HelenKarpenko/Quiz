using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz.Web.API.Models
{
  public class UserAnswers
  {
    public int TestId { get; set; }
    public string UserId { get; set; }
    public DateTime PassageDate { get; set; }
    public Dictionary<int, int[]> Answers { get; set; }

    //public UserAnswers(int testId, string userId, DateTime date, Dictionary<int, int[]> answers)
    //{
    //  TestId = testId;
    //  UserId = userId;
    //  PassageDate = date;
    //  Answers = answers;
    //}
  }
}
