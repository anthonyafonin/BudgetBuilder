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


        public ActionResult Update(TradesUpdateModel request)
        {

            if (ModelState.IsValid)
            {

                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();

                List<Trade> trades = db.Buildings.Find(request.BuildingID).Trades.ToList();

                return Json(new { Success = true, Trades = trades });
            }

            return Json(new { Success = false, Request = request });
        }

        [HttpPost]
        public ActionResult UpdateTrades(Building request)
        {
            if (ModelState.IsValid)
            {
                DateTime timestamp = DateTime.Now;
                request.DateModified = timestamp;

                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();

                List<Trade> trades = db.Buildings.Find(request.BuildingID).Trades.ToList();

                return Json(new { Success = true, Trades = trades });
            }
            return Json(new { Success = false });
        }



        [HttpPost]
        public ActionResult List(TradeListRequestModel request)
        {

            // Store current User Id
            int buildingId = request.BuildingID;

            // Select Buildings where foreign key is equal to current User Id
            var trades = db.Trades.Where(fk => fk.BuildingID == buildingId).ToList();

            return Json(new { Trades = trades });

        }

        [HttpPost]
        public ActionResult Create(Trade request)
        {
            bool success = false;

            if (ModelState.IsValid)
            {
                db.Trades.Add(request);
                db.SaveChanges();
                success = true;

                Building building = db.Buildings.Find(request.BuildingID);

                return Json(new { Success = success, Building = building });
            }

            return Json(new { Success = success });
        }

        // POST: Trades/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "TradesID,Category,SubCategory,MaterialCost,LaborCost,TradeBudget,TradeCost,BuildingID")] Trade tradeModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tradeModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = tradeModel.BuildingID });
            }
            ViewBag.BuildingID = new SelectList(db.Buildings, "BuildingID", "Title", tradeModel.BuildingID);
            return View(tradeModel);
        }

        // POST: Trades/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Trade tradeModel = db.Trades.Find(id);
            db.Trades.Remove(tradeModel);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = tradeModel.BuildingID });
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
