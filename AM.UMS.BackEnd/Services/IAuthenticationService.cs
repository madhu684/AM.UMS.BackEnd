using AM.UMS.BackEnd.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AM.UMS.BackEnd.Services
{
    public interface IAuthenticationService
    {
        User Authenticate(string username, string password);
    }
}
