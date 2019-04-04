using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Test2.Models;
using Test2.Models.DBModels;

namespace Test2.Controllers.DBControllers
{
    public class InvigilationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invigilations
        public async Task<ActionResult> Index()
        {
            var invigilations = db.Invigilations.Include(i => i.RelatedExamSession).Include(i => i.RelatedUser);
            return View(await invigilations.ToListAsync());
        }

        // GET: Invigilations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invigilation invigilation = await db.Invigilations.FindAsync(id);
            if (invigilation == null)
            {
                return HttpNotFound();
            }
            return View(invigilation);
        }

        /*
        // GET: Invigilations/Create
        public ActionResult Create()
        {
            ViewBag.ExamId = new SelectList(db.ExamSessions, "ExamId", "SubjectId");
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }
        */


        // GET: Invigilations/Create/5
        public   ActionResult Create(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ExamManager", "Clerk");

            }

            IEnumerable<ExamSession> exam = new List<ExamSession>(from e in db.ExamSessions where e.ExamId == id select e) ;

            ExamSession specificExam = new ExamSession();
            specificExam = (from e in db.ExamSessions where e.ExamId == id select e).SingleOrDefault();
            ViewBag.Subject = specificExam.SubjectId;


            ViewBag.ExamId  = new SelectList(exam, "ExamId", "SubjectId");
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }


        // POST: Invigilations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "InvigilationId,UserId,ExamId")] Invigilation invigilation)
        {

            if (ModelState.IsValid)
            {
                db.Invigilations.Add(invigilation);
                await db.SaveChangesAsync();
                return RedirectToAction("ExamManager","Clerk");
            }

            
            IEnumerable<ExamSession> exam = new List<ExamSession>(from e in db.ExamSessions where e.ExamId == invigilation.ExamId select e);
            
            ViewBag.ExamId = new SelectList(exam, "ExamId", "SubjectId");
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", invigilation.UserId);
            return View(invigilation);
        }

        // GET: Invigilations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invigilation invigilation = await db.Invigilations.FindAsync(id);
            if (invigilation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamId = new SelectList(db.ExamSessions, "ExamId", "SubjectId", invigilation.ExamId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", invigilation.UserId);
            return View(invigilation);
        }

        // POST: Invigilations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "InvigilationId,UserId,ExamId")] Invigilation invigilation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invigilation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ExamId = new SelectList(db.ExamSessions, "ExamId", "SubjectId", invigilation.ExamId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", invigilation.UserId);
            return View(invigilation);
        }

        // GET: Invigilations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invigilation invigilation = await db.Invigilations.FindAsync(id);
            if (invigilation == null)
            {
                return HttpNotFound();
            }
            return View(invigilation);
        }

        // POST: Invigilations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Invigilation invigilation = await db.Invigilations.FindAsync(id);
            db.Invigilations.Remove(invigilation);
            await db.SaveChangesAsync();
            return RedirectToAction("ExamManager", "Clerk");
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
