using Quiz.DAL.Entities.UserResults;
using System.Collections.Generic;

namespace Quiz.DAL.Entities
{
	public class Test
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public virtual ICollection<Question> Questions { get; set; }

		public virtual ICollection<TestResult> TestResults { get; set; }

		public Test()
		{
			TestResults = new HashSet<TestResult>();
		}
	}
}
