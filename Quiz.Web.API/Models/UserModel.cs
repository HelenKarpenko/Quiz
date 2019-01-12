using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz.Web.API.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
		public string Email { get; set; }
		public IEnumerable<string> Roles { get; set; }
	}
}
