using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Action;
using SIS.MvcFramework.Result;
using SULS.App.BindingModels;
using SULS.Models;
using SULS.Services;
using System.Security.Cryptography;
using System.Text;

namespace SULS.App.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (this.IsLoggedIn())
            {
                return Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginBindingModel userLoginModel)
        {
            if (this.IsLoggedIn())
            {
                return Redirect("/");
            }
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Users/Login");
            }

            string hashedPass = this.HashPassword(userLoginModel.Password);

            User userFromDb
                = this.userService
                .GetUserByUsernameAndPassword(userLoginModel.Username, hashedPass);

            if (userFromDb == null)
            {
                return this.Redirect("/Users/Login");
            }

            this.SignIn(userFromDb.Id, userFromDb.Username, userFromDb.Email);

            return this.Redirect("/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (this.IsLoggedIn())
            {
                return Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterBindingModel userRegisterModel)
        {
            if (this.IsLoggedIn())
            {
                return Redirect("/");
            }

            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Users/Register");
            }

            if (this.userService
                .AnyUserWithTheGivenUsernameOrEmail(userRegisterModel.Username, userRegisterModel.Email)
                ||
                userRegisterModel.Password != userRegisterModel.ConfirmPassword)
            {
                return this.Redirect("/Users/Register");
            }
            string hashedPass = this.HashPassword(userRegisterModel.Password);

            this.userService
                .CreateUser(userRegisterModel.Username, userRegisterModel.Email, hashedPass);

            return this.Redirect("/Users/Login");
        }

        public IActionResult Logout()
        {
            this.SignOut();

            return this.Redirect("/");
        }

        [NonAction]
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                return Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}