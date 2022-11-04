using StockApp.Portal.Areas.Identity.Data;

namespace StockApp.Portal.ViewModels
{
    public class MapRoleViewModel
    {
        public int Id { get; set; } 
        public List<RoleViewModel> Roles { get; set; }
        public List<UserViewModel> Users { get; set; }
        public List<MapUserRoleViewModel> UserRoles { get; set; }
    }

    public class MapUserRoleViewModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string[] RoleIds { get; set; }
    }
}
