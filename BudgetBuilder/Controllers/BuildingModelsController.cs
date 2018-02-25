using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BudgetBuilder.Models;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace BudgetBuilder.Controllers
{
    [Authorize]
    public class BuildingModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BuildingModels
        public ActionResult Index()
        {
            // Store current User Id
            string thisId = User.Identity.GetUserId();
     
            // Select BuildingsModels where foreign key is equal to current User Id
            var buildingModels = db.BuildingModels.Include(b => b.User).Include(t=>t.Trade).Where(fk => fk.ApplicationUserID == thisId).ToList();
    
            return View(buildingModels);
        }

        // GET: BuildingModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingModels buildingModels = db.BuildingModels.Find(id);
            if (buildingModels == null)
            {
                return HttpNotFound();
            }
            return View(buildingModels);
        }

        // GET: BuildingModels/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: BuildingModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BuildingModelsID,Title,Budget,BuildingProfit,ApplicationUserID")] BuildingModels buildingModels)
        {
            if (ModelState.IsValid)
            {
                db.BuildingModels.Add(buildingModels);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "FirstName", buildingModels.ApplicationUserID);
            return View(buildingModels);
        }

        // GET: BuildingModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingModels buildingModels = db.BuildingModels.Find(id);
            if (buildingModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "FirstName", buildingModels.ApplicationUserID);
            return View(buildingModels);
        }

        // POST: BuildingModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BuildingModelsID,Title,Budget,BuildingProfit,ApplicationUserID")] BuildingModels buildingModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buildingModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "FirstName", buildingModels.ApplicationUserID);
            return View(buildingModels);
        }

        // GET: BuildingModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingModels buildingModels = db.BuildingModels.Find(id);
            if (buildingModels == null)
            {
                return HttpNotFound();
            }
            return View(buildingModels);
        }

        // POST: BuildingModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BuildingModels buildingModels = db.BuildingModels.Find(id);
            db.BuildingModels.Remove(buildingModels);
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
