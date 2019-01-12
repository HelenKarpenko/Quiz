using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.BLL.DTO.UserResult
{
	public class TestResultDTO
	{
		public int Id { get; set; }

		public int TestId { get; set; }

		public string UserId { get; set; }

		public DateTime PassageDate { get; set; }

		public Dictionary<int, int> Answers { get; set; }

		public int Result { get; set; }

		public TestResultDTO()
		{
			//Details = new HashSet<ResultDetailsDTO>();
			Answers = new Dictionary<int, int>();
		}
	}
}
