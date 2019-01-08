using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz.Web.API.Models.UserResult
{
  public class ResultDetailsModel
  {
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public int AnswerId { get; set; }

    public int ResultId { get; set; }
  }
}
