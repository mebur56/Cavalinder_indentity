using Caalinder.AppService.Interfaces;
using Caalinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Caalinder.Controllers
{
    public class HorseController : Controller
    {
        List<string> errors = new List<string>();
        private readonly IHorseAppService _horseAppService;


        public HorseController(IHorseAppService horseAppService)
        {
            _horseAppService = horseAppService;
        }

        // GET: Horse
        public ActionResult Index()
        {
            return View();
        }

        // GET: Horse/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Horse/Create
        public ActionResult Create()
        {
            HorseViewModel h = new HorseViewModel();
            return View(h);
        }

        // POST: Horse/Create
        [HttpPost]
        public ActionResult Create(HorseViewModel horse)
        {
            try
            {
                errors = _horseAppService.Insert(horse);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Horse/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Horse/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Horse/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Horse/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
