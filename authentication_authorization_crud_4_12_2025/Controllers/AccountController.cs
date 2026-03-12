using authentication_authorization_crud_4_12_2025.Data;
using authentication_authorization_crud_4_12_2025.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using System.Text;

namespace authentication_authorization_crud_4_12_2025.Controllers
{
    public class AccountController : Controller
    {
        private readonly DB context;
        public AccountController(DB context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {

                var data = context.Users.Where(e => e.Username == model.Username).SingleOrDefault();



                if (data != null)
                {

                    bool isValid = (data.Username == model.Username && DecryptPassword(data.Password) == model.Password && data.IsActive == true);
                    if (isValid)
                    {



                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.Username)
                        //, new Claim(ClaimTypes.Role, model.Rolesname)
                        },
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        var principle = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
                        HttpContext.Session.SetString("Username", model.Username);
                        // HttpContext.Session.SetString("Rolesname", model.Rolesname);


                        var mydata = new Login_History()
                        {
                            UserLoggedin = model.Username,
                            LastLogin = DateTime.Now,
                            LastLogout = DateTime.Now

                        };
                        context.login_Histories.Add(mydata);
                        context.SaveChanges();

                        return RedirectToAction("Index", "main");
                    }
                    else if (data.IsActive == false)
                    {
                        TempData["errorAccountInactive"] = "Account Not Active !!!!";
                    }
                    else
                    {


                        TempData["errorPassword"] = "Invalid Password";
                        return View(model);
                    }

                }
                else
                {
                    TempData["errorUsername"] = "username not found";
                }
            }
            else
            {
                return View(model);
            }
            return View(model);
        }
        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;

            }
            else
            {
                byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(password);
                string encryptedPassword = Convert.ToBase64String(storePassword);
                return encryptedPassword;
            }

        }

        public static string DecryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] encryptedPassword = Convert.FromBase64String(password);
                string decryptedPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
                return decryptedPassword;
            }
        }
        public IActionResult LogOut()
        {
            //var data = context.Users.Where(e => e.Username == model1.Username).SingleOrDefault();
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var storedCookies = Request.Cookies.Keys;
            foreach (var cookies in storedCookies)
            {
                Response.Cookies.Delete(cookies);
            }

            var mydata = new Login_History()
            {
                UserLoggedin = User.Identity.Name,
                LastLogin = DateTime.Now,

            };
            context.login_Histories.Add(mydata);
            context.SaveChanges();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new User()
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = EncryptPassword(model.Password),
                    Mobile = model.Mobile,
                    Rolesname = model.Roles,
                    IsActive = model.IsActive,
                };
                context.Users.Add(data);
                context.SaveChanges();
                TempData["successMessage"] = "You are eligible to login ,Please use your credentials to login";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["errorMessage"] = "Empty form can't be submitted";
                return View(model);

            }
        }
        [AcceptVerbs("Post", "Get")]
        public IActionResult UserNameIsExist(string userName)
        {
            var data = context.Users.Where(e => e.Username == userName).SingleOrDefault();
            if (data != null)
            {
                return Json($"Username {userName} already exists");
            }
            else
            {
                return Json(true);
            }

            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(Models.ForgotPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
            {
                TempData["error"] = "Username (or email) and new password are required.";
                return View(model);
            }

            // Try to find user by username or email
            var user = context.Users
                .Where(u => u.Username == model.Username || u.Email == model.Username)
                .SingleOrDefault();

            if (user == null)
            {
                TempData["error"] = "User not found with provided username or email.";
                return View(model);
            }

            // Encrypt the new password and update user's password
            string encrypted = AccountController.EncryptPassword(model.Password);
            user.Password = encrypted;

            // Record the reset in forgot_Passwords table
            var fp = new Models.ForgotPassword()
            {
                Username = user.Username,
                Password = encrypted
            };
            context.forgot_Passwords.Add(fp);

            context.SaveChanges();

            TempData["success"] = "Password updated successfully. Please use the new password to login.";
            return RedirectToAction("Index", "main");
        }

    }
}
