using Quiz.DAL.Entities.UserResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DAL.Entities.User
{
	public class UserInfo
	{
		[Key]
		[ForeignKey("ApplicationUser")]
		public string Id { get; set; }
		public string Name { get; set; }
		public virtual ICollection<TestResult> TestResults { get; set; }

		public UserInfo()
		{
			TestResults = new HashSet<TestResult>();
		}

		public virtual ApplicationUser ApplicationUser { get; set; }
	}
}
