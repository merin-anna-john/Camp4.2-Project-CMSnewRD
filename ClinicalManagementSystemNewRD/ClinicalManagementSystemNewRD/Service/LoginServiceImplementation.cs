using ClinicalManagementSystemNewRD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalManagementSystemNewRD.Service
{
    public class LoginServiceImplementation:ILoginService
    {
        //field
        private readonly ILoginRepository _loginRepository;

        //Constructor Injection
        public LoginServiceImplementation(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        public async Task<int> AuthenticationAsync(int loginID, string password)
        {
            //Check business rules for validation
            return await _loginRepository.GetRoleIdAsync(loginID, password);
        }
    }
}
