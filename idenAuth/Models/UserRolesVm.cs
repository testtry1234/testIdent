using Microsoft.AspNetCore.Identity;

namespace idenAuth.Models{

    public class UserRolesVm{
        public UserRolesVm(){
            userRoles = new List<string>();
        }
        public IdentityUser user { get; set; }
        public IList<string> userRoles { get; set; }
    }
}