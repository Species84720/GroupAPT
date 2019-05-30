using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test2.Models;
using Test2.Models.DBModels;

namespace Test2.Controllers.DBControllers
{
    public class ShotsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Shots
        public async Task<ActionResult> Index()
        {
            var shots = db.Shots.Include(s => s.RelatedEnrollment);
            return View(await shots.ToListAsync());
        }

        // GET: Shots/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shot shot = await db.Shots.FindAsync(id);
            if (shot == null)
            {
                return HttpNotFound();
            }
            return View(shot);
        }

        // GET: Shots/Create
        public ActionResult Create()
        {
            ViewBag.EnrollmentId = new SelectList(db.Enrollments, "EnrollmentId", "StudentId");
            return View();
        }

        // POST: Shots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ShotId,EnrollmentId,ShotTiming,ImageLocation,ImageTitle")] Shot shot)
        {
            if (ModelState.IsValid)
            {
                db.Shots.Add(shot);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EnrollmentId = new SelectList(db.Enrollments, "EnrollmentId", "StudentId", shot.EnrollmentId);
            return View(shot);
        }

        // GET: Shots/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shot shot = await db.Shots.FindAsync(id);
            if (shot == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnrollmentId = new SelectList(db.Enrollments, "EnrollmentId", "StudentId", shot.EnrollmentId);
            return View(shot);
        }

        // POST: Shots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ShotId,EnrollmentId,ShotTiming,ImageLocation,ImageTitle")] Shot shot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shot).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EnrollmentId = new SelectList(db.Enrollments, "EnrollmentId", "StudentId", shot.EnrollmentId);
            return View(shot);
        }

        // GET: Shots/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shot shot = await db.Shots.FindAsync(id);
            if (shot == null)
            {
                return HttpNotFound();
            }
            return View(shot);
        }

        // POST: Shots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Shot shot = await db.Shots.FindAsync(id);
            db.Shots.Remove(shot);
            await db.SaveChangesAsync();
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
