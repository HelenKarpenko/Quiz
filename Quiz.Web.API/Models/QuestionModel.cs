using System.Collections.Generic;

namespace Quiz.Web.API.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public virtual ICollection<AnswerModel> Answers { get; set; }

        public int TestId { get; set; }
    }
}
