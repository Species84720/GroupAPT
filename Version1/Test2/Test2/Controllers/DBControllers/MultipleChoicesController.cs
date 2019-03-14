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
    public class MultipleChoicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MultipleChoices
        public async Task<ActionResult> Index()
        {
            var multipleChoices = db.MultipleChoices.Include(m => m.RelatedQuestion);
            return View(await multipleChoices.ToListAsync());
        }

        // GET: MultipleChoices/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MultipleChoice multipleChoice = await db.MultipleChoices.FindAsync(id);
            if (multipleChoice == null)
            {
                return HttpNotFound();
            }
            return View(multipleChoice);
        }

        // GET: MultipleChoices/Create
        public ActionResult Create()
        {
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "SubjectId");
            return View();
        }

        // POST: MultipleChoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MultipleChoiceId,OptionText1,OptionText2,OptionText3,OptionText4,CorrectChoice,QuestionId")] MultipleChoice multipleChoice)
        {
            if (ModelState.IsValid)
            {
                db.MultipleChoices.Add(multipleChoice);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "SubjectId", multipleChoice.QuestionId);
            return View(multipleChoice);
        }

        // GET: MultipleChoices/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MultipleChoice multipleChoice = await db.MultipleChoices.FindAsync(id);
            if (multipleChoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "SubjectId", multipleChoice.QuestionId);
            return View(multipleChoice);
        }

        // POST: MultipleChoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MultipleChoiceId,OptionText1,OptionText2,OptionText3,OptionText4,CorrectChoice,QuestionId")] MultipleChoice multipleChoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(multipleChoice).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "SubjectId", multipleChoice.QuestionId);
            return View(multipleChoice);
        }

        // GET: MultipleChoices/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MultipleChoice multipleChoice = await db.MultipleChoices.FindAsync(id);
            if (multipleChoice == null)
            {
                return HttpNotFound();
            }
            return View(multipleChoice);
        }

        // POST: MultipleChoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MultipleChoice multipleChoice = await db.MultipleChoices.FindAsync(id);
            db.MultipleChoices.Remove(multipleChoice);
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
