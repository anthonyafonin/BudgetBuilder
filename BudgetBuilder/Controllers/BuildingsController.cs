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
    public class BuildingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult List(BuildingRequestModel request)
        {
            // Store current User Id
            string userId = request.UserID;
     
            // Select BuildingsModels where foreign key is equal to current User Id
            var buildingModels = db.BuildingModels.Where(fk => fk.ApplicationUserID == userId)
                .Select(r => new { r.Title, r.Budget, r.DateAdded, r.DateModified, r.BuildingModelsID, r.Trade }).ToList();
    
            return Json(new { Buildings = buildingModels });
        }

        public ActionResult Details(BuildingRequestModel request)
        {
            if (request.BuildingModelsID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BuildingModels building = db.BuildingModels.Find(request.BuildingModelsID);
            if (building != null)
            {
                return Json(new { Building = building });
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        
        public ActionResult Create(BuildingModels request)
        {
            if (ModelState.IsValid)
            {
                db.BuildingModels.Add(request);
                db.SaveChanges();
            }

            return Json(new { Success = true });
        }

        // POST: BuildingModels/Edit/5
        [HttpPost]
        public ActionResult Edit(BuildingModels request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();

                BuildingRequestModel lookup = new BuildingRequestModel
                {
                    BuildingModelsID = request.BuildingModelsID
                };

                return Json(new { Success = true, Building = Details(lookup) });
            }
            return View(request);
        }

        // POST: BuildingModels/Delete/5
        [HttpPost, ActionName("Delete")]
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
