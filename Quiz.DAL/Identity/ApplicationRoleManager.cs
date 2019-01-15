using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Quiz.DAL.Entities.User;

namespace Quiz.DAL.Identity
{
	public class ApplicationRoleManager : RoleManager<ApplicationRole>
	{
		public ApplicationRoleManager(RoleStore<ApplicationRole> store)
					: base(store)
		{ }
	}
}
