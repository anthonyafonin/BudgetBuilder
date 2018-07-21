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
        public ActionResult Index(int? id)
        {

            ViewBag.BuildingModelID = id;

            // Return view of lists based on passed Building ID
            var tradeModels = db.TradeModels.Include(t => t.Building).Where(pk => pk.BuildingModelsID == id);
            return View(tradeModels.ToList());

        }

        // GET: TradeModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TradeModels tradeModels = db.TradeModels.Find(id);
            if (tradeModels == null)
            {
                return HttpNotFound();
            }
            return View(tradeModels);
        }


        // GET: TradeModels/Create
        public ActionResult Create(int? id)
        {
            ViewBag.ReturnID = id;
            ViewBag.BuildingModelsID = new SelectList(db.BuildingModels, "BuildingModelsID", "Title", id);
            // find and return teh TradeModel with the current id
            TradeModels tradeModels = db.TradeModels.Find(id);
            return View();
        }

        // POST: TradeModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: TradeModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TradeModels tradeModels = db.TradeModels.Find(id);
            if (tradeModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildingModelsID = new SelectList(db.BuildingModels, "BuildingModelsID", "Title", tradeModels.BuildingModelsID);
            return View(tradeModels);
        }

        // POST: TradeModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: TradeModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TradeModels tradeModels = db.TradeModels.Find(id);
            if (tradeModels == null)
            {
                return HttpNotFound();
            }
            return View(tradeModels);
        }

        // POST: TradeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
