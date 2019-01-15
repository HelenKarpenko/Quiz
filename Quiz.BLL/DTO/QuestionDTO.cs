using System.Collections.Generic;

namespace Quiz.BLL.DTO
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
    
        public virtual ICollection<AnswerDTO> Answers { get; set; }

        public int TestId { get; set; }
    }
}
