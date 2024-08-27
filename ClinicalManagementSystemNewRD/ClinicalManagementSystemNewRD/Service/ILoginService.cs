using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalManagementSystemNewRD.Service
{
    public interface ILoginService
    {
        //Authentication
        Task<int> AuthenticationAsync(int loginID, string password);
    }
}
