using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.BLL.DTO.UserResult
{
  public class ResultDetailsDTO
  {
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public int AnswerId { get; set; }

    public int ResultId { get; set; }
  }
}
