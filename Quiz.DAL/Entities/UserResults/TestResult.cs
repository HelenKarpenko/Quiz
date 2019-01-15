using Quiz.DAL.Entities.User;
using System;
using System.Collections.Generic;

namespace Quiz.DAL.Entities.UserResults
{
	public class TestResult
	{
		public int Id { get; set; }

		public int TestId { get; set; }
		public virtual Test Test { get; set; }

		public string UserId { get; set; }
		public virtual UserInfo User { get; set; }

		public DateTime PassageDate { get; set; }

		public virtual ICollection<ResultDetails> Details { get; set; }

		public int Result { get; set; }

		public TestResult()
		{
			Details = new HashSet<ResultDetails>();
		}
	}
}
