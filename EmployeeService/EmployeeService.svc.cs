using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using EmployeeService.App_Code.Repositories;
using EmployeeService.App_Code.Services;
using EmployeeService.App_Code.Services.Interfaces;
using Newtonsoft.Json;

namespace EmployeeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IEmployeeService
    {
        private readonly IEmplService employeeService;

        public Service1()
        {
            var repository = new EmployeeRepository();
            employeeService = new EmplService(repository);
        }

        public async Task<string> GetEmployeeById(int id)
        {
            try
            {
                var result = await employeeService.GetJsonEmployeeByIdAsync(id);

                if (string.IsNullOrEmpty(result))
                {
                    throw new WebFaultException<string>("User not found.", HttpStatusCode.NotFound);
                }

                return result;
            }
            catch (WebFaultException<string>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>("An error occurred while processing request.", HttpStatusCode.InternalServerError);
            }
        }

        public async Task EnableEmployee(int id, int enable)
        {
            try
            {
                if (!await employeeService.UpdateEmployeeAsync(id, enable))
                {
                    throw new WebFaultException<string>("User not found.", HttpStatusCode.NotFound);
                }                
            }
            catch (WebFaultException<string>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>("An error occurred while processing request.", HttpStatusCode.InternalServerError);
            }
        }
    }


}