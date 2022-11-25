using System.Security.Claims;

namespace Stock.MinimalAPI
{
    [Serializable]
    public class UserModel
    {
        public UserModel()
        {

        }
        public string userName { get; set; }
        public string password { get; set; }
        public List<Claim> claims { get; set; }
    }
}
