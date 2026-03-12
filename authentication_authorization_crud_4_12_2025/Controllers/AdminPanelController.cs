using authentication_authorization_crud_4_12_2025.Data;
using authentication_authorization_crud_4_12_2025.Migrations;
using authentication_authorization_crud_4_12_2025.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace authentication_authorization_crud_4_12_2025.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly DB Context;
        public AdminPanelController(DB Context)
        {
            this.Context = Context;
        }

        public IActionResult Index()
        {
            IEnumerable<Login_History> xData = Context.login_Histories;
            return View(xData);
        }
        public IActionResult GetData()
        {
            IEnumerable<User> xData = Context.Users;
            return View(xData);
        }
        public IActionResult Edit(int? UserId)
        {
            if (UserId == null || UserId == 0)
            {
                return NotFound();
            }

            var obj = Context.Users.Find(UserId);
            //SignUpUserViewModel s1=new SignUPUserViewModel();
            //s1.Users = mydb.Users.Find(UserId);

            if (obj == null)
            {
                return NotFound(obj);
            }

            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public IActionResult Edit(User model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var data = new User()
                    //{
                    //   Username = obj.Username
                    //   Email = obj.Email
                    //   Password = EncryptPassword(obj.Password)
                    //   Mobile = obj.Mobile
                    //   Rolesname = obj.Rolesname
                    //    IsActive = obj.IsActive
                    //};
                    model.Username = model.Username;
                    model.Email = model.Email;
                    model.Password = EncryptPassword(model.Password);
                    model.Mobile = model.Mobile;
                    model.Rolesname = model.Rolesname;
                    model.IsActive = model.IsActive;
                    Context.Users.Update(model);

                    Context.SaveChanges();
                    TempData["successMessage"] = "You Are Eligible To Login , " +
                        "Please Use Your Credentials To Login";
                    return RedirectToAction("GetData");
                }
                else
                {
                    return View(model);
                }
            }

            catch (Exception)
            {
                throw;
            }
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
        public ActionResult ViewProfile(int UserId)
        {
            IEnumerable<User> xData = Context.Users.ToList().Where(x => x.UserId == UserId);
            return View(xData);
        }
    }
}
