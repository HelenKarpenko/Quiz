using Quiz.BLL.DTO.UserResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.BLL.DTO.User
{
	public class UserDTO
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public string Name { get; set; }
		public virtual ICollection<string> Roles { get; set; }
		public virtual ICollection<TestResultDTO> TestResults { get; set; }

		public UserDTO()
		{
			TestResults = new HashSet<TestResultDTO>();
		}
	}
}
