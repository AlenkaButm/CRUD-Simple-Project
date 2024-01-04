using Employee_CRUD.Data;
using Employee_CRUD.Models;
using Employee_CRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext appDbContext;
        public EmployeeController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await appDbContext.Employees.ToListAsync();

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
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                DateEmployment = addEmployeeRequest.DateEmployment
            };

            await appDbContext.Employees.AddAsync(employee);
            await appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await appDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    DateOfBirth = employee.DateOfBirth,
                    DateEmployment = employee.DateEmployment,
                    DateDismissal = employee.DateDismissal,
                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel updateEmployeeRequest)
        {
            var employee = await appDbContext.Employees.FindAsync(updateEmployeeRequest.Id);

            if(employee != null)
            {
                employee.Name = updateEmployeeRequest.Name;
                employee.Email = updateEmployeeRequest.Email;
                employee.DateOfBirth = updateEmployeeRequest.DateOfBirth;
                employee.DateEmployment = updateEmployeeRequest.DateEmployment;
                employee.DateDismissal = updateEmployeeRequest.DateDismissal;

                await appDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel updateEmployeeRequest)
        {
            var employee = await appDbContext.Employees.FindAsync(updateEmployeeRequest.Id);

            if(employee != null)
            {
                appDbContext.Employees.Remove(employee);
                await appDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
