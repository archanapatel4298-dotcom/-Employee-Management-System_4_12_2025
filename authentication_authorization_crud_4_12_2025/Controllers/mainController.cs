using authentication_authorization_crud_4_12_2025.Data;
using authentication_authorization_crud_4_12_2025.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace authentication_authorization_crud_4_12_2025.Controllers
{
    public class mainController : Controller
    {
        private readonly DB db;
        public mainController(DB db)
        {
            this.db = db;
        }
        [Authorize]

        //index action method starts here
        public IActionResult Index()
        {
            IEnumerable<Chocolate> xData = db.chocolate_data.ToList();

            ViewBag.notiyfyy = TempData["MyNotifyy"];
            ViewBag.edited = TempData["Edited"];
            ViewBag.deleted = TempData["Deleted"];

            return View(xData);
        }

        //Create Action Method starts here
        public IActionResult Create()
        {
            List<string> dd_data = new List<string>()
            {
              "Cadbury","Lindt","Hershey","Nestle","Ferrero Rocher","Mars"
            };
            ViewBag.dropdown = dd_data;

            List<UserDropDownData> cl = new List<UserDropDownData>();
            cl = (from c in db.dropdown_users select c).ToList();
            //cl.Insert(0,new DropdownData{UserId = 0, Username = "--Select User Name--"});
            ViewBag.message = cl;

            return View(new Chocolate());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Chocolate xData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.chocolate_data.Add(xData);
                    db.SaveChanges();
                    TempData["MyNotifyy"] = "Data Inserted !! Successfully";
                    return RedirectToAction("Index");
                }
                return View(xData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Edit Action Method starts here
        public IActionResult Edit(int? id)
        {
            if (id == null && id == 0)
            {
                return NotFound();
            }
            var obj = db.chocolate_data.Find(id);

            List<string> dd_data_edit = new List<string>()
            {
                 "Cadbury","Lindt","Hershey","Nestle","Ferrero Rocher","Mars"
            };

            ViewBag.dropdown_edit = dd_data_edit;

            List<UserDropDownData> cl = new List<UserDropDownData>();
            cl = (from c in db.dropdown_users select c).ToList();
            //cl.Insert(0,new DropdownData{UserId = 0, Username = "--Select User Name--"});
            ViewBag.message = cl;

            if (id == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Chocolate obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.chocolate_data.Update(obj);
                    db.SaveChanges();
                    TempData["Edited"] = "Data Edited !! Successfuly";
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
            return View(obj);
        }

        //Delete Action Method starts here
        public IActionResult Delete(int? id)
        {
            if (id == null && id == 0)
            {
                return NotFound();
            }
            var obj = db.chocolate_data.Find(id);
            List<string> dd_data_delete = new List<string>()
            {
              "Cadbury","Lindt","Hershey","Nestle","Ferrero Rocher","Mars"
            };
            ViewBag.dropdown_delete = dd_data_delete;

            List<UserDropDownData> cl = new List<UserDropDownData>();
            cl = (from c in db.dropdown_users select c).ToList();
            //cl.Insert(0,new DropdownData{UserId = 0, Username = "--Select User Name--"});
            ViewBag.message = cl;

            if (obj == null)
            {
                return NotFound(obj);
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteConfirmed(int? id)
        {
            try
            {
                var obj = db.chocolate_data.Find(id);

                if (obj == null)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    db.chocolate_data.Remove(obj);
                    db.SaveChanges();
                    TempData["Deleted"] = "Data Deleted !! Successfuly";
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //ViewProfile Action Method starts here
        public IActionResult ViewProfile(int id)
        {
            try
            {
                var obj = db.chocolate_data.Where(x => x.ChocolateId == id).ToList();
                return View(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
