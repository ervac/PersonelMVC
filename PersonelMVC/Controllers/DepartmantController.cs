using PersonelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonelMVC.Controllers
{
    public class DepartmantController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            using (PersonelDbContext db = new PersonelDbContext())
            {
                List<Departmant> lstDepartman = db.Departmants.ToList();
                return View(lstDepartman);
            }
            
        }
        public ActionResult Create()
        {
            return View("DepartmantForm",new Departmant());
        }
   
        [HttpPost]
        public ActionResult Save(Departmant departmant)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmantForm", departmant);
            }

            using (PersonelDbContext db = new PersonelDbContext())
            {
                if (departmant.Id==0)
                db.Departmants.Add(departmant);
                else
                {
                    var updatedDepartmant = db.Departmants.Find(departmant.Id);
                    updatedDepartmant.Name = departmant.Name;
                }
                db.SaveChanges();              
            }
            return RedirectToAction("Index", "Departmant");
        }
        public ActionResult Update(int id)
        {
            using (PersonelDbContext db = new PersonelDbContext())
            {
                var departmant = db.Departmants.Find(id);
                if (departmant == null)
                    return HttpNotFound();
                else
                    return View("DepartmantForm", departmant);

            }

        }
        public ActionResult Delete(int id)
        {
            using (PersonelDbContext db = new PersonelDbContext())
            {
                var departmant = db.Departmants.Find(id);
                if (departmant == null)
                    return HttpNotFound();
                else
                {
                    db.Departmants.Remove(departmant);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
        }
    }
}