using Microsoft.AspNetCore.Authorization;
using VinEcomDomain.Enum;

namespace VinEcomAPI.CustomWebAttribute
{
    public class EnumAuthorizeAttribute : AuthorizeAttribute
    {
        public EnumAuthorizeAttribute(Role role)
        {
            Roles = role.ToString().Replace(" ", string.Empty);
        }
    }
}
