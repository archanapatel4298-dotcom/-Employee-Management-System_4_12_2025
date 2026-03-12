using authentication_authorization_crud_4_12_2025.Data;
using authentication_authorization_crud_4_12_2025.Models;
using Microsoft.AspNetCore.Mvc;

namespace authentication_authorization_crud_4_12_2025.Controllers
{
    public class LoginDataHistoryController : Controller
    {
        private readonly DB context;
        public LoginDataHistoryController(DB context)
        
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Login_History> xData = context.login_Histories;
            return View(xData);
        }
    }
}
