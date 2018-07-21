using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BudgetBuilder.Models;

namespace BudgetBuilder.Controllers
{
    public class TradesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TradeModels
        public ActionResult List(int? id)
        {

            ViewBag.BuildingModelID = id;

            // Return view of lists based on passed Building ID
            var tradeModels = db.TradeModels.Include(t => t.Building).Where(pk => pk.BuildingModelsID == id);
            return View(tradeModels.ToList());

        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "TradeModelsID,Category,SubCategory,MaterialCost,LaborCost,TradeBudget,TradeCost,BuildingModelsID")] TradeModels tradeModels)
        {
            if (ModelState.IsValid)
            {
                db.TradeModels.Add(tradeModels);
                db.SaveChanges();
                return RedirectToAction("Index", new {id = tradeModels.BuildingModelsID});
            }

            ViewBag.BuildingModelsID = new SelectList(db.BuildingModels, "BuildingModelsID", "Title", tradeModels.BuildingModelsID);
            return View(tradeModels);
        }

        // POST: TradeModels/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "TradeModelsID,Category,SubCategory,MaterialCost,LaborCost,TradeBudget,TradeCost,BuildingModelsID")] TradeModels tradeModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tradeModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = tradeModels.BuildingModelsID });
            }
            ViewBag.BuildingModelsID = new SelectList(db.BuildingModels, "BuildingModelsID", "Title", tradeModels.BuildingModelsID);
            return View(tradeModels);
        }

        // POST: TradeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TradeModels tradeModels = db.TradeModels.Find(id);
            db.TradeModels.Remove(tradeModels);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = tradeModels.BuildingModelsID });
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
