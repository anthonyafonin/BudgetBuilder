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
        public ActionResult List(TradeRequestModel request)
        {
            int? buildingId = request.BuildingID ?? null;

            if(buildingId != null)
            {
                // Select Buildings where foreign key is equal to current User Id
                var trades = db.Trades.Where(fk => fk.BuildingID == buildingId).ToList();

                return Json(new { Trades = trades });
            }
            return Json(new { Success = false });
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

        [HttpPost]
        public ActionResult Update(Trade request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();

                Trade trade = db.Trades.Find(request.TradeID);

                return Json(new { Success = true, Trade = trade });
            }
            return Json(new { Success = false });
        }

        [HttpPost]
        public ActionResult Delete(TradeRequestModel request)
        {
            Trade trade = db.Trades.Find(request.TradeID);
            db.Trades.Remove(trade);
            db.SaveChanges();
            return Json(new { Success = true });
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
