using employeeApp6._0.Data;
using employeeApp6._0.Models;
using employeeApp6._0.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace employeeApp6._0.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly mvcDemoDbContext mvcDemoDbContext;

        public EmployeesController(mvcDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
          var employees =  await mvcDemoDbContext.Employees.ToListAsync();
            return View(employees); 
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfbirth = addEmployeeRequest.DateOfbirth,
            };
           await mvcDemoDbContext.Employees.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("index");
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
          var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee != null)
            {
                var viewModel = new updateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfbirth = employee.DateOfbirth,
                };
                return await Task.Run(() => View("View",viewModel));
            }

            return RedirectToAction("index");
        }
        [HttpPost]
        public async Task<IActionResult> View(updateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);
            if(employee != null)
            {
                employee.Name = model.Name; 
                employee.Email = model.Email;   
                employee.Salary  = model.Salary;    
                employee.DateOfbirth  = model.DateOfbirth;
                employee.Department = model.Department;
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(updateEmployeeViewModel model)
        {
            var employee  = await  mvcDemoDbContext.Employees.FindAsync(model.Id);
            if(employee != null)
            {
                mvcDemoDbContext.Employees.Remove(employee);    
                await mvcDemoDbContext.SaveChangesAsync();


                return RedirectToAction("index");
            }
          return  RedirectToAction("index");  
        }
    }
}
