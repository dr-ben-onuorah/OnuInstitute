using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    //localhost:portnum/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext DBContext;
        public EmployeesController(ApplicationDbContext dbContext) //constructor // to get the DBContext from the program.cs
        {
            this.DBContext  = dbContext;
        }
               

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = DBContext.Employees.ToList(); 
            return Ok(allEmployees);
        }




        [HttpGet]
        [Route("{id:int}")] //guid (generate multiple uinque id)
        public IActionResult GetAllEmployeesById(int id) //Guide id
        {
            var employee = DBContext.Employees.Find(id);
            if(employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }



        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmplDto)
        {
            //convert the Emply-Dto to Emply-Entity
            var employeeEntity = new Employee()
            {
                Name = addEmplDto.Name,
                Email = addEmplDto.Email,
                Phone = addEmplDto.Phone,
                Salary = addEmplDto.Salary,
            };

            DBContext.Employees.Add(employeeEntity);
            DBContext.SaveChanges();
            return Ok(employeeEntity);

        }



        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateEmployees(int id, UpdateEmployeeDto updateEmplDto) //Guide id
        {
            //first find the employee
            var employee = DBContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }
            employee.Name = updateEmplDto.Name;
            employee.Email = updateEmplDto.Email;
            employee.Phone = updateEmplDto.Phone;
            employee.Salary= updateEmplDto.Salary;

            DBContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:int}")] //guid (generate multiple uinque id)
        public IActionResult DeleteEmployee(int id) //Guide id
        {
            var employee = DBContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }
            DBContext.Employees.Remove(employee);
            DBContext.SaveChanges();
            return Ok();
        }
    }
}
