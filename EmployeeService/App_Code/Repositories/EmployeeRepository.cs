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
using System.Web;

namespace EmployeeService.App_Code.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string connectionString = @ConfigurationManager.ConnectionStrings["Emploee"].ConnectionString;

        public DataTable GetEmployeeById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = String.Format("SELECT * FROM Employee WHERE ID = {0}", id);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        public DataTable GetSubordinatesForEmployee(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = String.Format("SELECT * FROM Employee WHERE ManagerId = {0}", id);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        public bool UpdateEmployee(int id, int enabled)
        {
            bool isUpdated = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string updateQuery = "UPDATE Employee SET Enable = @Enable WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Enable", enabled);
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

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