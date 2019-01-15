using System;
using System.Collections.Generic;

namespace Quiz.Web.API.Models.UserResult
{
	public class TestResultModel
	{
		public int Id { get; set; }

		public int TestId { get; set; }

		public string TestName { get; set; }

		public string UserName { get; set; }

		public string UserId { get; set; }

		public DateTime PassageDate { get; set; }
		
		public virtual ICollection<ResultDetailsModel> Details { get; set; }

		public int Result { get; set; }

		public int MaxResult { get; set; }

		public TestResultModel()
		{
			Details = new HashSet<ResultDetailsModel>();
		}
	}
}
