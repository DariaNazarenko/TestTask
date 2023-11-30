using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.App_Code.Services.Interfaces
{
    public interface IEmplService
    {
        Task<string> GetJsonEmployeeByIdAsync(int id);
        Task<bool> UpdateEmployeeAsync(int id, int enabled);
    }
}
