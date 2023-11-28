using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.App_Code.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        DataTable GetEmployeeById(int id);
        DataTable GetSubordinatesForEmployee(int id);
        bool UpdateEmployee(int id, int enabled);
    }
}
