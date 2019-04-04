using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Test2.Models;
using Test2.Models.DBModels;

namespace Test2.Controllers.DBControllers
{
    public class TeachingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       

        // GET: Teachings
        [Authorize(Roles = "Clerk")]
        public async Task<ActionResult> Index()
        {
            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user  select u.DepartmentId).FirstOrDefault();

            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId==dept || d.DepartmentParentId==dept select d.DepartmentId );

            List<string> usersInSameDept = new List<string>(from u in db.Users 
                where depts.Contains(u.RelatedDepartment.DepartmentId) select u.Id);
             

            
var teachings = (from t in db.Teachings.Include(t=>t.Examinable).Include(t =>t.Examiner )
                                                         where usersInSameDept.Contains(t.ExaminerId)  select t);
                                                          
 


            return View(await teachings.ToListAsync() );
        }

        // GET: Teachings/Details/5
        [Authorize(Roles = "Clerk")]
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
        [Authorize(Roles = "Clerk")]
        public ActionResult Create()
        {
            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();

            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);
            List<Subject> subjects = new List<Subject>(from s in db.Subjects where 
                depts.Contains( s.RelatedDepartment.DepartmentId) select s);

            List<ApplicationUser> users = 
                new List<ApplicationUser>
                    (from u in db.Users where depts.Contains(u.RelatedDepartment.DepartmentId)   select u);
             

            ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName");
            ViewBag.ExaminerId = new SelectList(users, "Id", "FirstName");
            return View();

        }

        // POST: Teachings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TeachingId,ExaminerId,SubjectId")] Test2.Models.DBModels.Teaching teaching)
        {
            if (ModelState.IsValid)
            {
                db.Teachings.Add(teaching);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();

            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);
            List<Subject> subjects = new List<Subject>(from s in db.Subjects
                where
                    depts.Contains(s.RelatedDepartment.DepartmentId)
                select s);

            List<ApplicationUser> users =
                new List<ApplicationUser>
                    (from u in db.Users where depts.Contains(u.RelatedDepartment.DepartmentId) select u);


            ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName", teaching.SubjectId);
            ViewBag.ExaminerId = new SelectList(users, "Id", "FirstName", teaching.ExaminerId);
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
            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();

            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);
            List<Subject> subjects = new List<Subject>(from s in db.Subjects
                where
                    depts.Contains(s.RelatedDepartment.DepartmentId)
                select s);

            List<ApplicationUser> users =
                new List<ApplicationUser>
                    (from u in db.Users where depts.Contains(u.RelatedDepartment.DepartmentId) select u);


            ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName", teaching.SubjectId);
            ViewBag.ExaminerId = new SelectList(users, "Id", "FirstName", teaching.ExaminerId);
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
            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();

            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);
            List<Subject> subjects = new List<Subject>(from s in db.Subjects
                where
                    depts.Contains(s.RelatedDepartment.DepartmentId)
                select s);

            List<ApplicationUser> users =
                new List<ApplicationUser>
                    (from u in db.Users where depts.Contains(u.RelatedDepartment.DepartmentId) select u);


            ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName", teaching.SubjectId);
            ViewBag.ExaminerId = new SelectList(users, "Id", "FirstName", teaching.ExaminerId);
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
