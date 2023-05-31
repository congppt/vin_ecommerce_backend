using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomViewModel.Global;

namespace VinEcomInterface.IService
{
    public interface IBaseService
    {
        Task<AuthorizedViewModel?> AuthorizeAsync(SignInViewModel vm);
    }
}
