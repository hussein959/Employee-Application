using Microsoft.AspNetCore.Mvc;
using webWithSQL.Models;
namespace webWithSQL.Controllers
{
    public class EmployeeController : Controller
    {
        HRDatabaseContext dbContext = new HRDatabaseContext();

        public IActionResult Index(string SortField, string CurrentSortField,string SortDirection,string searchByName)
        {
            var employees=GetEmployees();
            if(!string.IsNullOrEmpty(searchByName))
                //ToLower method to search for smaill and capital letters
                employees = employees.Where(e=>e.EmployeeName.ToLower().Contains(searchByName.ToLower())).ToList();
            return View(this.sortEmployees(employees, SortField, CurrentSortField, SortDirection));
        }

        private List<Employee> GetEmployees()
        {
            //to bring from one table
            //List<Employee> Emploees = dbContext.Employees.ToList();

            //to bring list of the data from data base
            var employees = (from employee in dbContext.Employees
                             join department in dbContext.Departments on employee.Departmentid equals department.DepartmentId
                             select new Employee
                             {
                                 EmployeeID = employee.EmployeeID,
                                 EmployeeName = employee.EmployeeName,
                                 EmployeeNumber = employee.EmployeeNumber,
                                 DOB = employee.DOB,
                                 HiringDate = employee.HiringDate,
                                 GrossSalary = employee.GrossSalary,
                                 NetSalary = employee.NetSalary,
                                 Departmentid = employee.Departmentid,
                                 DepartmentName = department.DepartmentName
                             }).ToList();
            //send the list to the view
            return employees;
        }

        public IActionResult Create()
        {
            ViewBag.Departments = this.dbContext.Departments.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee model)
        {
            ModelState.Remove("EmployeeID");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");
            if (ModelState.IsValid)
            {
                dbContext.Employees.Add(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = this.dbContext.Departments.ToList();
            return View();

        }
        public IActionResult Edit(int ID)
        {
            //to catch the id of employee
            Employee data = this.dbContext.Employees.Where(e => e.EmployeeID == ID).FirstOrDefault();
            ViewBag.Departments=this.dbContext.Departments.ToList();
            return View("Create",data);
        }
        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            ModelState.Remove("EmployeeID");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");
            if (ModelState.IsValid)
            {
                dbContext.Employees.Update(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = this.dbContext.Departments.ToList();
            return View("Create",model);

        }
        public IActionResult Delete(int ID)
        {
            Employee data = this.dbContext.Employees.Where(e => e.EmployeeID == ID).FirstOrDefault();
            if (data != null)
            {
                dbContext.Employees.Remove(data);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        private List<Employee> sortEmployees(List<Employee> employees, string sortfield, string currentSortField, string sortDirection )
        {    
            //just the wayes of sorting
            if(string.IsNullOrEmpty(sortfield))
            {
                ViewBag.SortField = "EmployeeNumber";
                ViewBag.sortDirection = "Asc";
            }
            else
            {
                if (currentSortField == sortfield)
                    ViewBag.sortDirection = sortDirection == "Asc" ? "Desc" : "Asc";
                else ViewBag.sortDirection = "Asc";
                ViewBag.SortField=sortfield;
                
            }
            //this is the sorting Method
            var propertyInfo =typeof(Employee).GetProperty(ViewBag.SortField);
            if (ViewBag.SortDirection == "Asc")
            {
                employees = employees.OrderBy(e => propertyInfo.GetValue(e, null)).ToList();
            }
            else employees = employees.OrderByDescending(e => propertyInfo.GetValue(e, null)).ToList();
            return employees;

        }

    }
}
