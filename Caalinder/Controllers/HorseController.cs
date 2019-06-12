using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Caalinder.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Caalinder.Controllers
{
    public class HorseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Horse
        public ActionResult Index()
        {
            IEnumerable<HorseModel> horses = new List<HorseModel>();
            string userid = User.Identity.GetUserId();
            horses = db.HorseModels.Where(h => h.ApplicationUserId == userid);
            return View(horses.ToList());
        }

        // GET: Horse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HorseModel horseModel = db.HorseModels.Find(id);
            if (horseModel == null)
            {
                return HttpNotFound();
            }
            return View(horseModel);
        }

        // GET: Horse/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Gender,HorseBrand,HorseBirth,Description,ApplicationUserID")] HorseModel horseModel)
        {
            ApplicationUser CurrentUser = System.Web.HttpContext.Current.GetOwinContext()
                   .GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                horseModel.ApplicationUserId = CurrentUser.Id;
                db.HorseModels.Add(horseModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "name", horseModel.ApplicationUserId);
            return View(horseModel);
        }

        // GET: Horse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HorseModel horseModel = db.HorseModels.Find(id);
            if (horseModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "name", horseModel.ApplicationUserId);
            return View(horseModel);
        }

        // POST: Horse/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Gender,HorseBrand,HorseBirth,Description,ApplicationUserID")] HorseModel horseModel)
        {
              ApplicationUser CurrentUser = System.Web.HttpContext.Current.GetOwinContext()
                   .GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                horseModel.ApplicationUserId = CurrentUser.Id;
                db.Entry(horseModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "name", horseModel.ApplicationUserId);
            return View(horseModel);
        }

        // GET: Horse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HorseModel horseModel = db.HorseModels.Find(id);
            if (horseModel == null)
            {
                return HttpNotFound();
            }
            return View(horseModel);
        }

        // POST: Horse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HorseModel horseModel = db.HorseModels.Find(id);
            db.HorseModels.Remove(horseModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
