using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using banking_system.Models;

namespace banking_system.Controllers
{
    public class TransfersController : Controller
    {
        private CustomersEntities db = new CustomersEntities();

        // GET: Transfers
        public ActionResult Index()
        {
            var transfers = db.Transfers.Include(t => t.Customer).Include(t => t.Customer1);
            return View(transfers.ToList());
        }

        // GET: Transfers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transfer transfer = db.Transfers.Find(id);
            if (transfer == null)
            {
                return HttpNotFound();
            }
            return View(transfer);
        }

        // GET: Transfers/Create
        public ActionResult Create()
        {
            //ViewBag.from_customer_id = new SelectList(db.Customers, "id", "name");
            ViewBag.to_customer_id = new SelectList(db.Customers, "id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? fromCustomerId, Transfer transfer ,Customer customer)
        {
            if (ModelState.IsValid)
            {
                fromCustomerId = customer.id;
                var sender = db.Customers.Find(fromCustomerId);
                var receiver = db.Customers.Find(transfer.to_customer_id);

                if (sender != null && receiver != null)
                {
                    if (sender.current_balance >= transfer.amount)
                    {
                        sender.current_balance -= transfer.amount;
                        receiver.current_balance += transfer.amount;

                        db.Transfers.Add(transfer);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Customers");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Insufficient balance in the sender's account.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid sender or receiver.");
                }
            }

            //ViewBag.from_customer_id = new SelectList(db.Customers, "id", "name", transfer.from_customer_id);
            ViewBag.to_customer_id = new SelectList(db.Customers, "id", "name", transfer.to_customer_id);
            return View("Index");
        }

        // GET: Transfers/Edit/5
      
       

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
