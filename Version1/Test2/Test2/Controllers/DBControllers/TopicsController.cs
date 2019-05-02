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
    public class TopicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Topics
        public async Task<ActionResult> Index()
        {
            var topics = db.Topics.Include(t => t.RelatedSubject);
            return View(await topics.ToListAsync());
        }

        // GET: Topics/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = await db.Topics.FindAsync(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Topics/Create
        [Authorize(Roles="Examiner")]
        public ActionResult Create(string subject)
        {

            // limiting choices of Subject to those of Examiner
            string TeacherId = User.Identity.GetUserId();

           // List<string> specificsubjects = new List<string>(from t in db.Teachings where t.ExaminerId == TeacherId select t.SubjectId);
            

            IEnumerable<Subject> subjectchosen = new List<Subject>(from s in db.Subjects where s.SubjectId == subject select s);
            ViewBag.Subject = db.Subjects.Where(s => s.SubjectId == subject).FirstOrDefault().SubjectName;
            ViewBag.SubjectId = new SelectList(subjectchosen, "SubjectId", "SubjectName");

            Topic t =new Topic();
            t.SubjectId = subject;

            return View(t);
        }

        // POST: Topics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TopicId,TopicName,SubjectId")] Topic topic)
        {
            
            if (ModelState.IsValid)
            {
                db.Topics.Add(topic);
                await db.SaveChangesAsync();
                return RedirectToAction("EditQuestions", "Examiner", new { subject = topic.SubjectId });
            }


            // limiting choices of Subject to those of Examiner
            string TeacherId = User.Identity.GetUserId();

            //List<string> specificsubjects = new List<string>(from t in db.Teachings where t.ExaminerId == TeacherId select t.SubjectId);
            IEnumerable<Subject> subjectlist = new List<Subject>(from s in db.Subjects where s.SubjectId==topic.SubjectId select s);

            ViewBag.Subject = topic.SubjectId;
            ViewBag.SubjectId = new SelectList(subjectlist,"SubjectId", "SubjectName", topic.SubjectId);

            return View(topic);
        }

        // GET: Topics/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = await db.Topics.FindAsync(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", topic.SubjectId);
            return View(topic);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TopicId,TopicName,SubjectId")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("EditQuestions", "Examiner", new { subject = topic.SubjectId });
            }
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", topic.SubjectId);
            return View(topic);
        }

        // GET: Topics/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = await db.Topics.FindAsync(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Topic topic = await db.Topics.FindAsync(id);
            string t_subject = topic.SubjectId;
            db.Topics.Remove(topic);
            await db.SaveChangesAsync();
            return RedirectToAction("EditQuestions", "Examiner", new { subject = t_subject });
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
