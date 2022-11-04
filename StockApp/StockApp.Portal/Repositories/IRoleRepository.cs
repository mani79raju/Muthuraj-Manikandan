using Microsoft.AspNetCore.Identity;
using StockApp.Portal.Areas.Identity.Data;
using StockApp.Portal.ViewModels;

namespace StockApp.Portal.Repositories
{
    public interface IRoleRepository
    {
        List<IdentityRole> GetRoles();
        IdentityRole GetRoleById(string roleId);
        string AddRole(List<RoleViewModel> roles);
        string UpdateRole(RoleViewModel role);
        string DeleteRole(string roleId);
        string MapUserRole(MapUserRoleViewModel mapRole);
        List<MapUserRoleViewModel> GetMappedUserRoles();
    }
}
