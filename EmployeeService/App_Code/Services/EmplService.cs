using EmployeeService.App_Code.Models;
using EmployeeService.App_Code.Repositories;
using EmployeeService.App_Code.Repositories.Interfaces;
using EmployeeService.App_Code.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;

namespace EmployeeService.App_Code.Services
{
    public class EmplService : IEmplService
    {
        private readonly IEmployeeRepository repository = new EmployeeRepository();

        public EmplService(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public string GetJsonEmployeeById(int id)
        {
            string employeeJson = string.Empty;
            DataTable employeeData = repository.GetEmployeeById(id);

            if (employeeData.Rows.Count > 0)
            {
                List<EmployeeModel> subordinates = new List<EmployeeModel>();

                DataTable subordinatesData = repository.GetSubordinatesForEmployee(id);
                foreach (DataRow subRow in subordinatesData.Rows)
                {
                    subordinates.Add(new EmployeeModel
                    {
                        Id = Convert.ToInt32(subRow["id"]),
                        Name = subRow["Name"].ToString(),
                        ManagerId = Convert.ToInt32(subRow["ManagerID"]),
                        Enable = Convert.ToBoolean(subRow["enable"])
                    });
                }

                EmployeeModel fullEmployee = MapEmployeeAndSubordinates(employeeData, subordinates);

                employeeJson = JsonConvert.SerializeObject(fullEmployee, Formatting.None);
            }

            return employeeJson;
        }

        public bool UpdateEmployee(int id, int enabled)
        {
            return repository.UpdateEmployee(id, enabled);
        }

        private EmployeeModel MapEmployeeAndSubordinates(DataTable employeeDataTable, List<EmployeeModel> subordinateMap)
        {
            DataRow employee = employeeDataTable.Rows[0];

            EmployeeModel employeeWithSubordinates = new EmployeeModel
            {
                Id = Convert.ToInt32(employee["id"]),
                Name = employee["Name"].ToString(),
                ManagerId = Convert.ToInt32(employee["ManagerID"]),
                Enable = Convert.ToBoolean(employee["enable"]),
                Subordinates = subordinateMap
            };

            return employeeWithSubordinates;
        }
    }
}