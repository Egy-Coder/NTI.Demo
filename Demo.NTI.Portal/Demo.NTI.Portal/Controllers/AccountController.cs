using Demo.NTI.BLL.ViewModel;
using Demo.NTI.DAL.Extend;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.NTI.Portal.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _userManager 
            , SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }


        #region Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {

            var user = await userManager.FindByNameAsync(login.UserName);

            if(user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid UserName Or Password");

                }

                return View();
            }

            return RedirectToAction(nameof(Registration));
        }


        #endregion


        #region Registration

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationVM model)
        {

            if(ModelState.IsValid)
            {

                //+"@example.com",
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    IsAgree = model.IsAgree
                };

                //var result = await userManager.CreateAsync(user); // Not Encrypt
                var result = await userManager.CreateAsync(user,model.Password); // Encrypt


                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }

            return View();
        }


        #endregion



        #region SignOut

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }


        #endregion
    }
}
