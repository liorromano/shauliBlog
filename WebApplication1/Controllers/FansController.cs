using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FansController : Controller
    {
        private FanDBContext db = new FanDBContext();

        // GET: Fans
        public ActionResult Index(string fanGender, string searchString, string Time)
        {// the searchString is the name of the fan
            var GenderList = new List<string>();
            //takes all the gendet that apper in the fans table 
            var GenderQry = from d in db.Fans
                            orderby d.Gender
                            select d.Gender;
            //merge if there're the same value (will be only one famele and one male)
            GenderList.AddRange(GenderQry.Distinct());
            //display the result as a list
            ViewBag.fanGender = new SelectList(GenderList);

            //In this loop filters the result of the fans accroding to the given parameters
            var fans = from m in db.Fans
                       select m;
            //every "if" reduce more lines from the fans table
            if (!String.IsNullOrEmpty(searchString))
            {//holds the line of the table that the given name apper there.
                fans = fans.Where(s => s.Name.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(fanGender))
            {
                fans = fans.Where(x => x.Gender == fanGender);
            }
            if (!string.IsNullOrEmpty(Time))
            {
                fans = fans.Where(x => x.TimeInClub.ToString() == Time);
            }


            return View(fans);
        }

        // GET: Fans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // GET: Fans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,LastName,Birthday,Gender,City,TimeInClub")] Fan fan)
        {
            if (ModelState.IsValid)
            {
                db.Fans.Add(fan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fan);
        }
    
        // GET: Fans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // POST: Fans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,LastName,Birthday,Gender,City,TimeInClub")] Fan fan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fan);
        }

        // GET: Fans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // POST: Fans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fan fan = db.Fans.Find(id);
            db.Fans.Remove(fan);
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
       
        public JsonResult getCity()
        {//this func goes over the fans table and makes a list of the fans cities
            List<cities> address = new List<cities>();
            foreach (var item in db.Fans)
            {
                cities place = new cities();
                if (!String.IsNullOrEmpty(item.City))
                {
                    place.addr = item.City;
          
                    address.Add(place);
                }
            }
            return Json(address, JsonRequestBehavior.AllowGet);
        }
        public class cities
        {
            public string addr { get; set; }

        }
    }
}
