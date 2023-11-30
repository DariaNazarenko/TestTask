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
        Task<DataTable> GetEmployeeByIdAsync(int id);
        Task<DataTable> GetSubordinatesForEmployeeAsync(int id);
        Task<bool> UpdateEmployeeAsync(int id, int enabled);
    }
}
