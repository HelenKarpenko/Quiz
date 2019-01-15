using Microsoft.AspNet.Identity;
using Quiz.DAL.Entities.User;

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
