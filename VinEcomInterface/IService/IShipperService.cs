using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomViewModel.Global;

namespace VinEcomInterface.IService
{
    public interface IShipperService : IUserService
    {
        Task<AuthorizedViewModel?> AuthorizeAsync(SignInViewModel vm);
    }
}
