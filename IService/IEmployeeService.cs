using Employee_Webapi.Model;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Webapi.IService
{
    public interface IEmployeeService
    {
        public Task<IActionResult> Save(Employee employee);
        public Task<IActionResult> Update(Employee employee);
        public Task<IActionResult> Delete(String Id);
        public Task<IActionResult> View(String Id);
        Task<List<Employee>> GetEmployeeList();
    }
}
