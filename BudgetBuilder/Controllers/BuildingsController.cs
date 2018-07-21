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

        [HttpPost]
        public ActionResult List(BuildingRequestModel request)
        {
            // Store current User Id
            string userId = request.UserID;
     
            // Select BuildingsModels where foreign key is equal to current User Id
            var buildingModels = db.BuildingModels.Where(fk => fk.ApplicationUserID == userId)
                .Select(r => new { r.Title, r.Budget, r.DateAdded, r.DateModified, r.BuildingModelsID, r.Trade }).ToList();
    
            return Json(new { Buildings = buildingModels });
        }

        [HttpPost]
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

        [HttpPost]
        public ActionResult Create(BuildingModels request)
        {
            bool success = false;

            if (ModelState.IsValid)
            {
                DateTime timestamp = DateTime.Now;
                request.DateAdded = timestamp;
                request.DateModified = timestamp;

                db.BuildingModels.Add(request);
                db.SaveChanges();
                success = true;

                BuildingModels building = db.BuildingModels.Find(request.BuildingModelsID);

                return Json(new { Success = success, Building = building });
            }

            return Json(new { Success = success });
        }

        [HttpPost]
        public ActionResult Update(BuildingModels request)
        {
            if (ModelState.IsValid)
            {
                DateTime timestamp = DateTime.Now;
                request.DateModified = timestamp;

                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();

                BuildingModels building = db.BuildingModels.Find(request.BuildingModelsID);

                return Json(new { Success = true, Building = building });
            }
            return Json(new { Success = false });
        }

        [HttpPost]
        public ActionResult Delete(BuildingRequestModel request)
        {
            BuildingModels building = db.BuildingModels.Find(request.BuildingModelsID);
            db.BuildingModels.Remove(building);
            db.SaveChanges();
            return Json(new { Success = true } );
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
