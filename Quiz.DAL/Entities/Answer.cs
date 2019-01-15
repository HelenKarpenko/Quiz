using Quiz.DAL.Entities.UserResults;
using System.Collections.Generic;

namespace Quiz.DAL.Entities
{
	public class Answer
	{
		public int Id { get; set; }

		public string Text { get; set; }

		public bool IsCorrect { get; set; }

		public int QuestionId { get; set; }

		public virtual Question Question { get; set; }

		public virtual ICollection<ResultDetails> ResultDetails { get; set; }

		public Answer()
		{
			ResultDetails = new HashSet<ResultDetails>();
		}
	}
}
