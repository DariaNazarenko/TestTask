using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.App_Code.Services.Interfaces
{
    public interface IEmplService
    {
        string GetJsonEmployeeById(int id);
        bool UpdateEmployee(int id, int enabled);
    }
}
