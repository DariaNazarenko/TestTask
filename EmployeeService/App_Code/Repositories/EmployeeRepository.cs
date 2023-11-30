using EmployeeService.App_Code.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeService.App_Code.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string connectionString = @ConfigurationManager.ConnectionStrings["Emploee"].ConnectionString;

        public async Task<DataTable> GetEmployeeByIdAsync(int id)
        {
            string queryString = "SELECT * FROM Employee WHERE ID = @EmployeeId";
            DataTable employee = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@EmployeeId", id);

                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    employee.Load(reader);
                }
            }

            return employee;
        }

        public async Task<DataTable> GetSubordinatesForEmployeeAsync(int id)
        {
            string queryString = "SELECT * FROM Employee WHERE ManagerId = @EmployeeId";
            DataTable employees = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@EmployeeId", id);

                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    employees.Load(reader);
                }
            }

            return employees;
        }

        public async Task<bool> UpdateEmployeeAsync(int id, int enabled)
        {
            bool isUpdated = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string updateQuery = "UPDATE Employee SET Enable = @Enable WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Enable", enabled);
                    command.Parameters.AddWithValue("@Id", id);

                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                       isUpdated = true;
                    }
                }
            }

            return isUpdated;
        }
    }
}