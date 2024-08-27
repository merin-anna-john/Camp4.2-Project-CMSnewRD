using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalManagementSystemNewRD.Repository
{
    public interface ILoginRepository
    {
        Task<int> GetRoleIdAsync(int loginID, string password);

    }
}
