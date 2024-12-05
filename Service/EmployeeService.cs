using Employee_Webapi.IService;
using Employee_Webapi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Employee_Webapi.Service
{
    public class EmployeeService : IEmployeeService 
    {
        private readonly string _connectionString;

        public EmployeeService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IActionResult> Save(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("SP_Employee", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        //cmd.Parameters.AddWithValue("@Id", employee.Id ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Name", employee.Name);
                        cmd.Parameters.AddWithValue("@Emp_Id", employee.Number);
                        cmd.Parameters.AddWithValue("@DOB", employee.DOB);
                        cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                        cmd.Parameters.AddWithValue("@Country", employee.Country);
                        cmd.Parameters.AddWithValue("@Action", "INSERT");
                        cmd.Parameters.AddWithValue("@Skills", string.Join(",", employee.Skills));

                        await cmd.ExecuteNonQueryAsync();
                    }

                    return new OkResult();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public async Task<IActionResult> Update(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("SP_Employee", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", employee.Id);
                        cmd.Parameters.AddWithValue("@Name", employee.Name);
                        cmd.Parameters.AddWithValue("@Emp_Id", employee.Number);
                        cmd.Parameters.AddWithValue("@DOB", employee.DOB);
                        cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                        cmd.Parameters.AddWithValue("@Country", employee.Country);
                        cmd.Parameters.AddWithValue("@Action", employee.Action);
                        cmd.Parameters.AddWithValue("@Skills", string.Join(",", employee.Skills));

                        await cmd.ExecuteNonQueryAsync();
                    }

                    return new OkResult();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public async Task<IActionResult> Delete(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_DeleteEmployee", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@Action", "DELETE");

                    conn.Open();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return new OkResult();
        }
        public async Task<IActionResult> View(string Id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("SP_Employee", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        //cmd.Parameters.AddWithValue("@Id", employee.Id);
                        //cmd.Parameters.AddWithValue("@Name", employee.Name);
                        //cmd.Parameters.AddWithValue("@Emp_Id", employee.Number);
                        //cmd.Parameters.AddWithValue("@DOB", employee.DOB);
                        //cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                        //cmd.Parameters.AddWithValue("@Country", employee.Country);
                        //cmd.Parameters.AddWithValue("@Action", employee.Action);
                        //cmd.Parameters.AddWithValue("@Skills", string.Join(",", employee.Skills));

                        await cmd.ExecuteNonQueryAsync();
                    }

                    return new OkResult();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public async Task<List<Employee>> GetEmployeeList()
        {
            List<Employee> employeeList = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_Employee", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Action", "GETLIST");
                        cmd.Parameters.AddWithValue("@Name", "");
                        cmd.Parameters.AddWithValue("@Emp_Id", "");
                        cmd.Parameters.AddWithValue("@DOB", "");
                        cmd.Parameters.AddWithValue("@Gender", "");
                        cmd.Parameters.AddWithValue("@Country", "");
                        cmd.Parameters.AddWithValue("@Skills", "");

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Employee employee = new Employee
                                {
                                    Id = reader["Id"].ToString(),
                                    Number = reader["Emp_Id"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    DOB = reader["DOB"].ToString(),
                                    Gender = reader["Gender"].ToString(),
                                    Country = reader["Country"].ToString(),
                                    Skills = reader["Skills"].ToString().Split(',').ToList()
                                };

                                employeeList.Add(employee);
                            }
                        }
                    }

                    return employeeList; 
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving employee list", ex);
                }
            }
        }


    }
}
