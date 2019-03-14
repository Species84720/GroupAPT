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
    public class TeachingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Teachings
        public async Task<ActionResult> Index()
        {
            var teachings = db.Teachings.Include(t => t.Examinable).Include(t => t.Examiner);
            return View(await teachings.ToListAsync());
        }

        // GET: Teachings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teaching teaching = await db.Teachings.FindAsync(id);
            if (teaching == null)
            {
                return HttpNotFound();
            }
            return View(teaching);
        }

        // GET: Teachings/Create
        public ActionResult Create()
        {
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");
            ViewBag.ExaminerId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Teachings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TeachingId,ExaminerId,SubjectId")] Teaching teaching)
        {
            if (ModelState.IsValid)
            {
                db.Teachings.Add(teaching);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", teaching.SubjectId);
            ViewBag.ExaminerId = new SelectList(db.Users, "Id", "FirstName", teaching.ExaminerId);
            return View(teaching);
        }

        // GET: Teachings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teaching teaching = await db.Teachings.FindAsync(id);
            if (teaching == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", teaching.SubjectId);
            ViewBag.ExaminerId = new SelectList(db.Users, "Id", "FirstName", teaching.ExaminerId);
            return View(teaching);
        }

        // POST: Teachings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TeachingId,ExaminerId,SubjectId")] Teaching teaching)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teaching).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", teaching.SubjectId);
            ViewBag.ExaminerId = new SelectList(db.Users, "Id", "FirstName", teaching.ExaminerId);
            return View(teaching);
        }

        // GET: Teachings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teaching teaching = await db.Teachings.FindAsync(id);
            if (teaching == null)
            {
                return HttpNotFound();
            }
            return View(teaching);
        }

        // POST: Teachings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Teaching teaching = await db.Teachings.FindAsync(id);
            db.Teachings.Remove(teaching);
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
