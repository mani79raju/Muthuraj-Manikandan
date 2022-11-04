using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Portal.ViewModels;
using StockApp.Portal.Repositories;

namespace StockApp.Portal.Controllers
{
	public class AdminController : Controller
	{
		IUnitOfWork UnitOfWork { get; set; }

		public AdminController(IUnitOfWork unitOfWork)
		{
			UnitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Roles()
		{
			var roles = UnitOfWork.Role.GetRoles().Select(x => new RoleViewModel() { Id = x.Id, Name = x.Name }).ToList();
			return View(roles);
		}

        public IActionResult MapUserRoles()
        {
            MapRoleViewModel model = new MapRoleViewModel();
            model.Users = UnitOfWork.User.GetUsers().Select(x => new UserViewModel() { Id = x.Id, FirstName = x.FirstName, Email = x.Email, LastName = x.LastName}).ToList();
            model.Roles = UnitOfWork.Role.GetRoles().Select(x => new RoleViewModel() { Id = x.Id, Name = x.Name }).ToList();
            model.UserRoles = UnitOfWork.Role.GetMappedUserRoles();
            return View(model);
        }

        [Authorize(Policy = "Admin")]
        public void SaveRole(string[] roleNames)
        {
            if (roleNames != null && roleNames.Length > 0)
            {
               var roleNameList = roleNames.Select(x => new RoleViewModel() { Name = x }).ToList();
               UnitOfWork.Role.AddRole(roleNameList);
            }
            //return RedirectToAction("Roles");
        }

        public string UpdateRole(RoleViewModel model)
		{
            UnitOfWork.Role.UpdateRole(model);
            return "Success";
		}

        public string DeleteRole(string roleId)
        {
            UnitOfWork.Role.DeleteRole(roleId);
            return "Success";
        }

        public string MapUserRole(MapUserRoleViewModel model)
        {
            if (model.UserId != null)
            {
                UnitOfWork.Role.MapUserRole(model);
            }
            return "Success";
        }
    }
}
