using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Test2.Models;
using Test2.Models.DBModels;

namespace Test2.Controllers.DBControllers
{
    public class SubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Subjects
        public    ActionResult Index()
        {

            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();

            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);

            List<Subject> subjectlist = new List<Subject>(from s in db.Subjects
                join d in db.Departments on s.DepartmentId equals d.DepartmentId
                where
                    depts.Contains(s.RelatedDepartment.DepartmentId)
                select s);


            
            return View(subjectlist);
        }

        // GET: Subjects/Details/5
        public async Task<ActionResult> Details(string id)
        {


            if (id == null)
            {
                return RedirectToAction("Details", "Subjects");
            }

            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();

            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);

            Subject testSubject =new Subject();

            testSubject = await db.Subjects.FindAsync(id);


            // prevent a clerk from viewing a subject not in his/her department
            if (testSubject == null || !depts.Contains(testSubject.RelatedDepartment.DepartmentId))
            {
                return RedirectToAction("Management", "Dashboard");
            }
             

            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);

        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();

            List<Department> departments = new List<Department>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d);


            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SubjectId,SubjectName,DepartmentId,Credits")] Subject subject)
        {


            if (ModelState.IsValid)
            {
                db.Subjects.Add(subject);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();

            List<Department> departments = new List<Department>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d);


            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName", subject.DepartmentId);
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return RedirectToAction("Index", "Subjects");
            }

            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();

            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);

            Subject testSubject = new Subject();

            testSubject = await db.Subjects.FindAsync(id);


            // prevent a clerk from viewing a subject not in his/her department
            if (testSubject == null || !depts.Contains(testSubject.RelatedDepartment.DepartmentId))
            {
                return RedirectToAction("Management", "Dashboard");
            }



            List<Department> departments = new List<Department>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d);


            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName", subject.DepartmentId);
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SubjectId,SubjectName,DepartmentId,Credits")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();


            List<Department> departments = new List<Department>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d);


            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName", subject.DepartmentId);
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            

            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();

            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);

            Subject testSubject = new Subject();

            testSubject = await db.Subjects.FindAsync(id);


            // prevent a clerk from viewing a subject not in his/her department
            if (testSubject == null || !depts.Contains(testSubject.RelatedDepartment.DepartmentId))
            {
                return RedirectToAction("Management", "Dashboard");
            }


            return View(testSubject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Subject subject = await db.Subjects.FindAsync(id);
            db.Subjects.Remove(subject);
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
