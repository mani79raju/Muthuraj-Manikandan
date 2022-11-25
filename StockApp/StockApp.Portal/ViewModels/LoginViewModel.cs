
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StockApp.Portal.ViewModels
{
    [Serializable]
    public class LoginViewModel
    {
        public string userName { get; set; }
        public string password { get; set; }
        public List<Claim> claims { get; set; }

    }
}
