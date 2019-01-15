using System.Collections.Generic;

namespace Quiz.Web.API.Models
{
	public class TestModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public virtual ICollection<QuestionModel> Questions { get; set; }
	}
}
