using Quiz.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace Quiz.DAL.Identity
{
	public class ApplicationUserManager : UserManager<ApplicationUser>
	{
		public ApplicationUserManager(IUserStore<ApplicationUser> store)
				: base(store)
		{
		}
	}
}
