using Quiz.Web.API.Models.UserResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz.Web.API.Models
{
    public class UserModel
    {
        public string Id { get; set; }
		public string Name { get; set; }
        public string Username { get; set; }
		public string Email { get; set; }
		public virtual IEnumerable<string> Roles { get; set; }
		public virtual ICollection<TestResultModel> TestResults { get; set; }

		public UserModel()
		{
			TestResults = new HashSet<TestResultModel>();
		}
	}
}
