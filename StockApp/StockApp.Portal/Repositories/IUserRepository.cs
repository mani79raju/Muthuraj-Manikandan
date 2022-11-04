using StockApp.Portal.Areas.Identity.Data;
using StockApp.Portal.ViewModels;

namespace StockApp.Portal.Repositories
{
    public interface IUserRepository
    {
        ICollection<ApplicationUser> GetUsers();

        ApplicationUser GetUserById(string userId);

        ApplicationUser GetUserByEmail(string useremail);

        List<ApplicationUser> SearchUsers(string keyword);

        string UpdateUser(UserViewModel user);
    }
}
