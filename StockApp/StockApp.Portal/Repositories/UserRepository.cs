using Microsoft.EntityFrameworkCore;
using StockApp.Portal.Areas.Identity.Data;
using StockApp.Portal.ViewModels;

namespace StockApp.Portal.Repositories
{
    public class UserRepository : IUserRepository
    {
        public ApplicationDbContext Context { get; set; }
        public UserRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public ICollection<ApplicationUser> GetUsers()
        {
            return Context.Users.ToList();
        }

        public ApplicationUser GetUserByEmail(string useremail)
        {
            return Context.Users.Where(x => x.Email.ToLower() == useremail.ToLower()).ToList().FirstOrDefault();
        }

        public ApplicationUser GetUserById(string userId)
        {
            return Context.Users.FromSqlRaw("sp_GetUserById {0}", userId).ToList().FirstOrDefault();
        }

        public List<ApplicationUser> SearchUsers(string keyword)
        {
            return Context.Users.FromSqlRaw("sp_SearchUser {0}", keyword).ToList();
        }

        public string UpdateUser(UserViewModel user)
        {
            if (user != null)
            {
                var dbUser = Context.Users.Where(x => x.Id == user.Id).FirstOrDefault();
                if (dbUser != null)
                {
                    dbUser.FirstName = user.FirstName;
                    dbUser.LastName = user.LastName;
                    Context.SaveChanges();
                }
            }
            return "Success";
        }
    }
}
