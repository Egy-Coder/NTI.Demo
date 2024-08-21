using Demo.NTI.BLL.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Demo.NTI.DAL.Entities;
using Demo.NTI.BLL.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Demo.NTI.BLL.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace Demo.NTI.Portal.Controllers
{

    [Authorize(Roles = "Admin , Hr")]
    public class EmployeeController : Controller
    {

        private readonly IEmployeeService employeeService;
        private readonly IDepartmentService departmentService;
        private readonly ICityService cityService;

        public EmployeeController(IEmployeeService employeeService , 
            IDepartmentService departmentService,
            ICityService cityService)
        {
            this.employeeService = employeeService;
            this.departmentService = departmentService;
            this.cityService = cityService;
        }


        public async Task<ActionResult> Index()
        {
            var data = await employeeService.GetAllAsync(a => a.IsActive == true , 1 , 10 , null , new List<Expression<Func<Employee, object>>> { a => a.Department});
            return View(data);
        }


        public async Task<IActionResult> Details(Guid id)
        {
            var data = await employeeService.GetEmployeeByIdAsync(a => a.IsActive == true && a.Id == id ,
                new List<Expression<Func<Employee, object>>> { a => a.Department });

            return View(data);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeVM employee)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    if(employee.Cv != null && employee.Image != null)
                    {
                        employee.CvName = FileUploader.Upload("/Docs/Cvs", employee.Cv);
                        employee.ImageName = FileUploader.Upload("/Docs/Imgs", employee.Image);
                    }

                    employeeService.CreateEmployee(employee);
                }
                else
                {
                    return View(employee);
                }


            }
            catch (Exception ex)
            {
                // log
                //SeriLog , NLog
                // message
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await employeeService.GetEmployeeByIdAsync(a => a.Id == id && a.IsActive == true , 
                new List<Expression<Func<Employee, object>>> { a => a.Department});

            ViewBag.DepartmentList = new SelectList(await departmentService.GetDepartmentsAsync(), "Id", "Name", result.DepartmentId);

            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeVM employee)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    employeeService.UpdateEmployee(employee);
                }
                else
                {
                    ViewBag.DepartmentList = new SelectList(await departmentService.GetDepartmentsAsync(), "Id", "Name", employee.DepartmentId);
                    return View(employee);
                }


            }
            catch (Exception ex)
            {
                // log
                //SeriLog , NLog
                // message
            }

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await employeeService.GetEmployeeByIdAsync(a => a.Id == id && a.IsActive == true, 
                new List<Expression<Func<Employee, object>>> { a => a.Department });

            ViewBag.DepartmentList = new SelectList(await departmentService.GetDepartmentsAsync(), "Id", "Name", result.DepartmentId);

            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeVM employee)
        {
            try
            {



                FileUploader.Remove("/Docs/Cvs/", employee.CvName);
                FileUploader.Remove("/Docs/Imgs/", employee.ImageName);

                employeeService.DeleteEmployee(employee);
                //var existingEmployee = await employeeService.GetEmployeeByIdAsync(a => a.Id == employee.Id);

                //if(existingEmployee.IsActive == true ) 
                //{
                //    existingEmployee.IsActive = false;
                //    employeeService.DeleteEmployee(employee);
                //}
            }
            catch (Exception ex)
            {
                // log
                //SeriLog , NLog
                // message
            }

            return RedirectToAction(nameof(Index));
        }



        #region Ajax Call

        [HttpPost]
        public async Task<JsonResult> getCitiesByCountryId(int cntryId)
        {
            var cities = await cityService.GetCities(a => a.CountryId == cntryId);
            return Json(cities);
        }



        [HttpPost]
        public async Task<IActionResult> LoadData()
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 10;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int pageNumber = (skip / pageSize) + 1;

            Expression<Func<Employee, bool>> filter = null;
            Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null;

            // Search filter
            if (!string.IsNullOrEmpty(searchValue))
            {
                filter = e => e.Name.Contains(searchValue) || e.Email.Contains(searchValue);
            }

            // Sorting
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
            {
                orderBy = q => sortColumnDirection == "asc"
                    ? q.OrderBy(e => EF.Property<object>(e, sortColumn))
                    : q.OrderByDescending(e => EF.Property<object>(e, sortColumn));
            }

            var data = await employeeService.GetAllAsync(
                filter: filter,
                page: pageNumber,
                pageSize: pageSize,
                orderBy: orderBy
            );

            int recordsTotal = await employeeService.CountAsync();

            return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
        }


        #endregion
    }
}
