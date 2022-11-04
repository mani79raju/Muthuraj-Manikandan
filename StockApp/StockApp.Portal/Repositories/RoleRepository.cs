using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StockApp.Portal.Areas.Identity.Data;
using StockApp.Portal.ViewModels;

namespace StockApp.Portal.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public ApplicationDbContext Context { get; set; }
        public RoleRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public List<IdentityRole> GetRoles()
        {
            return Context.Roles.ToList();
        }

        public IdentityRole GetRoleById(string roleId)
        {
            return Context.Roles.Where(x => x.Id == roleId).FirstOrDefault();
        }

        public string AddRole(List<RoleViewModel> roles)
        {
            if (roles != null && roles.Count > 0)
            {
                foreach (var role in roles)
                {
                    Context.Roles.Add(new IdentityRole() { Name = role.Name });
                }
                Context.SaveChanges();
            }
            return "Roles Added";
        }

        public string UpdateRole(RoleViewModel role)
        {
            var userRole = GetRoleById(role.Id);
            if (userRole != null)
            {
                userRole.Name = role.Name;
                Context.SaveChanges();
            }

            return "Role Updated";
        }

        public string DeleteRole(string roleId)
        {
            var userRole = GetRoleById(roleId);
            if (userRole != null)
            {
                Context.Roles.Remove(userRole);
                Context.SaveChanges();
            }

            return "Role Deleted";
        }

        public string MapUserRole(MapUserRoleViewModel mapRole)
        {
            if (mapRole != null && mapRole.RoleIds.Length > 0)
            {
                RemoveAllMappedUserRoles(mapRole);
                foreach (var roleId in mapRole.RoleIds)
                {
                    Context.UserRoles.Add(new IdentityUserRole<string>() { RoleId = roleId, UserId = mapRole.UserId });
                }
                Context.SaveChanges();
            }

            return "Roles mapped to user";
        }

        public void RemoveAllMappedUserRoles(MapUserRoleViewModel mapRole)
        {
            if (mapRole != null && !string.IsNullOrEmpty(mapRole.UserId))
            {
                Context.UserRoles.RemoveRange(GetMappedUserRolesByUserId(mapRole.UserId));
                Context.SaveChanges();
            }
        }

        public List<MapUserRoleViewModel> GetMappedUserRoles()
        {
            return Context.UserRoles.Select(x => new MapUserRoleViewModel() { UserId = x.UserId, RoleId = x.RoleId }).ToList();
        }

        public List<IdentityUserRole<string>> GetMappedUserRolesByUserId(string userId)
        {
            return Context.UserRoles.Where(x => x.UserId == userId).ToList();
        }

        public List<IdentityUserRole<string>> GetMappedUserRolesByRoleId(string roleId)
        {
            return Context.UserRoles.Where(x => x.RoleId == roleId).ToList();
        }
    }
}
