using Microsoft.AspNet.Identity.EntityFramework;

namespace Quiz.DAL.Entities.User
{
	public class ApplicationUser : IdentityUser
	{
		public virtual UserInfo UserInfo { get; set; }
	}
}
