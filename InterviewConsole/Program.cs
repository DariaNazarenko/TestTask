using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //int milliseconds = 10000;
            //Thread.Sleep(milliseconds);

            //// Create an instance of HttpClient
            //var client = new HttpClient();

            //// Make a GET request
            //HttpResponseMessage responseGet = await client.GetAsync("http://localhost:64014/EmployeeService.svc/GetEmployeeById?id=2");

            //string data = await responseGet.Content.ReadAsStringAsync();
            //Console.WriteLine(data);


            // Make a PUT request
            var client = new HttpClient();
            var payload = new
            {
                enable = 1
            };

            string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage responsePut = await client.PutAsync("http://localhost:64014/EmployeeService.svc/EnableEmployee?id=2", content);

            string dataPut = await responsePut.Content.ReadAsStringAsync();
            Console.WriteLine(dataPut);
        }

        private static DataTable GetQueryResult(string query)
        {
            var dt = new DataTable();

            using (var connection = new SqlConnection("Data Source=(local);Initial Catalog=Test;User ID=sa;Password=pass@word1; "))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }
    }
}
