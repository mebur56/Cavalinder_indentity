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

namespace Caalinder.Controllers
{
    public class MatchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        // GET: Match
        public ActionResult Index()
        {

            IEnumerable<HorseModel> horses = new List<HorseModel>();
            string userid = User.Identity.GetUserId();
            horses = db.HorseModels.Where(h => h.ApplicationUserId != userid);
            return View(horses.ToList());
        }
        
      
        // GET: Match/Details/5
        public ActionResult SelecionarCavalo(int? id)
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
            IEnumerable<HorseModel> horses = new List<HorseModel>();
            string userid = User.Identity.GetUserId();
            horses = db.HorseModels.Where(h => h.ApplicationUserId == userid);
            ViewBag.Name = new SelectList(horses,"Id","Name", horseModel.Name);
            
            return View(horseModel);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchModel matchModel = db.MatchModels.Find(id);
            if (matchModel == null)
            {
                return HttpNotFound();
            }
            return View(matchModel);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Horse1Id,Horse2Id,Like1,Like2,Match")] MatchModel matchModel)
        {
            if (ModelState.IsValid)
            {
                db.MatchModels.Add(matchModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(matchModel);
        }

        // GET: Match/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchModel matchModel = db.MatchModels.Find(id);
            if (matchModel == null)
            {
                return HttpNotFound();
            }
            return View(matchModel);
        }

        // POST: Match/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Horse1Id,Horse2Id,Like1,Like2,Match")] MatchModel matchModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matchModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(matchModel);
        }

        // GET: Match/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchModel matchModel = db.MatchModels.Find(id);
            if (matchModel == null)
            {
                return HttpNotFound();
            }
            return View(matchModel);
        }

        // POST: Match/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MatchModel matchModel = db.MatchModels.Find(id);
            db.MatchModels.Remove(matchModel);
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
