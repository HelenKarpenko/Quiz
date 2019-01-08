using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Quiz.DAL.Entities.UserResults;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Quiz.DAL.Entities
{
  public class ApplicationUser : IdentityUser
  {
    public string Name { get; set; }
    public ICollection<TestResult> TestResults { get; set; }

    public ApplicationUser()
    {
      TestResults = new HashSet<TestResult>();
    }

    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    {
      var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
      return userIdentity;
    }
  }
}
