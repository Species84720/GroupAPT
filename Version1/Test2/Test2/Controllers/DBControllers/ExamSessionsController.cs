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
    public class ExamSessionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExamSessions
        public async Task<ActionResult> OldIndex()
        {  //if you need to create a bunch of  ExamSessions without logging in as a Clerk 
            var examSessions = db.ExamSessions.Include(e => e.RelatedLocation).Include(e => e.RelatedSubject);
            return View(await examSessions.ToListAsync());
        }

        // GET: ExamSessions
        [Authorize(Roles = "Clerk")]
        public async Task<ActionResult> Index()
        {
            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();
            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);


            List<string> subjects = new List<string>(from s in db.Subjects where depts.Contains(s.RelatedDepartment.DepartmentId) select s.SubjectId);

            var sessions = (from e in db.ExamSessions.Include(e => e.RelatedLocation).Include(e => e.RelatedSubject) where subjects.Contains(e.SubjectId) select e);



            //var examSessions = db.ExamSessions.Include(e => e.RelatedLocation).Include(e => e.RelatedSubject);
            return View( await sessions.ToListAsync());
        }

        // GET: ExamSessions/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamSession examSession = await db.ExamSessions.FindAsync(id);
            if (examSession == null)
            {
                return HttpNotFound();
            }
            return View(examSession);
        }

        // GET: ExamSessions/Create
        [Authorize(Roles = "Admin, Clerk")]
        public ActionResult Create()
        {
            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();
            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);


            List<Subject> subjects = new List<Subject>(from s in db.Subjects where depts.Contains(s.RelatedDepartment.DepartmentId) select s);

            
            ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName");
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Campus");
            return View();
        }

        // POST: ExamSessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ExamId,SubjectId,LocationId,ExamDateTime,ExamEndTime,QuestionAmount,AccessCode,CodeIssueDateTime,FullyCorrected,MaxMark,MinMark,AvgMark,NumOfParticipants,NumOfFails")] ExamSession examSession)
        {
            if (ModelState.IsValid)
            {
                db.ExamSessions.Add(examSession);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }


            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();
            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);


            List<Subject> subjects = new List<Subject>(from s in db.Subjects where depts.Contains(s.RelatedDepartment.DepartmentId) select s);


            ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName");
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Campus", examSession.LocationId);
            
            return View(examSession);
        }



        
        public ActionResult ExamDateChange(string idd)
        {
            ExamSession examSession =new ExamSession();
            examSession = db.ExamSessions.Find(idd);

            examSession.ExamDateTime = null;

            db.Entry(examSession).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Edit", "ExamSessions", new{ id=idd});
        }



        public ActionResult ExamEndChange(string idd)
        {
            ExamSession examSession = new ExamSession();
            examSession = db.ExamSessions.Find(idd);

            examSession.ExamEndTime = null;

            db.Entry(examSession).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Edit", "ExamSessions", new { id = idd });
        }





        // GET: ExamSessions/Edit/5
        [Authorize(Roles = "Clerk")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamSession examSession = await db.ExamSessions.FindAsync(id);
            if (examSession == null)
            {
                return RedirectToAction("ExamManager","Clerk");
            }
            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();

            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);

            if (!depts.Contains(examSession.RelatedSubject.DepartmentId))
            {
                return RedirectToAction("Management", "Dashboard");
            }

            List<Subject> subjects = new List<Subject>(from s in db.Subjects where depts.Contains(s.RelatedDepartment.DepartmentId) select s);

            ViewBag.Start = examSession.ExamDateTime;

            var Locations = db.Locations.Select(x => x.Campus).Distinct().ToList();
            ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName");
            ViewBag.LocationId = new SelectList(Locations, examSession.LocationId);

            if (examSession.LocationId != null)
            {
                var LocationCall = db.Locations.Where(x => x.LocationId == examSession.LocationId).FirstOrDefault();
                ViewBag.LocationCall = new string[]
                {
                    LocationCall.Campus,
                    LocationCall.Building,
                    LocationCall.Floor,
                    LocationCall.Block,
                    LocationCall.Room
                };
            }

            return View(examSession);
        }

        [Authorize(Roles = "Clerk")]
        [HttpPost]
        public async Task<ActionResult> AjaxPost(string campus, string building, string floor, string block, string room)
        {
            IQueryable<Location> Locations = db.Locations;

            if (campus != "null")
                Locations = Locations.Where(x => x.Campus == campus);

            int LocationId = 0;
            List<string> returnValue = new List<string>();
            if (building != "")
            {
                if (building != "null")
                    Locations = Locations.Where(x => x.Building == building);
                if (floor != "")
                {
                    if (floor != "null")
                        Locations = Locations.Where(x => x.Floor == floor);
                    if (block != "")
                    {
                        if (block != "null")
                            Locations = Locations.Where(x => x.Block == block);
                        if (room != "")
                        {
                            LocationId = Locations.Where(x => x.Room == room).Select(x => x.LocationId).FirstOrDefault();
                        }
                        else
                        {
                            returnValue = Locations.Select(x => x.Room).ToList();
                        }
                    }
                    else
                    {
                        returnValue = Locations.Select(x => x.Block).Distinct().ToList();
                    }
                }
                else
                {
                    returnValue = Locations.Select(x => x.Floor).Distinct().ToList();
                }
            }
            else
            {
                returnValue = Locations.Select(x => x.Building).Distinct().ToList();
            }

            var response = new
            {
                success = true,
                location = returnValue,
                Id = LocationId
            };

            return Json(response);
        }

        // POST: ExamSessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ExamId,SubjectId,LocationId,ExamDateTime,ExamEndTime,QuestionAmount,AccessCode,CodeIssueDateTime,FullyCorrected,MaxMark,MinMark,AvgMark,NumOfParticipants,NumOfFails")] ExamSession examSession)
        {
            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();

            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);
 
            if (ModelState.IsValid)
            {
               
           /*
            if (!depts.Contains(examSession.RelatedSubject.DepartmentId))
            {
                return RedirectToAction("Management", "Dashboard");
            }
            */

                db.Entry(examSession).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("ExamManager", "Clerk");
            }

            List<Subject> subjects = new List<Subject>(from s in db.Subjects where depts.Contains(s.RelatedDepartment.DepartmentId) select s);


            ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName");
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Campus", examSession.LocationId);

            return View(examSession);
        }


        


        // GET: ExamSessions/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            ExamSession examSession = await db.ExamSessions.FindAsync(id);
            if (examSession == null)
            {
                return RedirectToAction("Index");
            }

            /*
            
            string user = User.Identity.GetUserId();

            int? dept = (from u in db.Users where u.Id == user select u.DepartmentId).FirstOrDefault();

            List<int> depts = new List<int>(from d in db.Departments where d.DepartmentId == dept || d.DepartmentParentId == dept select d.DepartmentId);

            if (depts.Contains(examSession.RelatedSubject.DepartmentId))
                {
                    return RedirectToAction("Management", "Dashboard");
                }
                */



            return View(examSession);
        }

        // POST: ExamSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ExamSession examSession = await db.ExamSessions.FindAsync(id);
            db.ExamSessions.Remove(examSession);
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
