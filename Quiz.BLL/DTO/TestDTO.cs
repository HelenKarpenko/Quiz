using System.Collections.Generic;

namespace Quiz.BLL.DTO
{
  public class TestDTO
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<QuestionDTO> Questions { get; set; }
  }
}
