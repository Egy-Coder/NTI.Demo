using Demo.NTI.BLL.Service;
using Demo.NTI.BLL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.NTI.Portal.Controllers
{

    [Authorize(Roles = "Admin , Hr")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService _departmentService)
        {
            departmentService = _departmentService;
        }

        public async Task<IActionResult> Index()
        {

            //// ViewBag
            //ViewBag.x = "hi i'm view bag";

            ////// ViewData
            //ViewData["y"] = "hi i'm view data";

            ////// TempData
            //TempData["z"] = "hi i'm temp data";


            //string[] names = { "Ahmed", "Ali", "Sara" };
            //ViewBag.Names = names;

            //return RedirectToAction(nameof(Test));

            var data = await departmentService.GetDepartmentsAsync();

            return View(data);
        }

        [HttpPost]
        public IActionResult Create(DepartmentVM model)
        {
            departmentService.CreateDepartment(model);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult Update(DepartmentVM model)
        {
            departmentService.UpdateDepartment(model);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult Delete(DepartmentVM model)
        {
            departmentService.DeleteDepartment(model);
            return RedirectToAction(nameof(Index));
        }

    }
}
