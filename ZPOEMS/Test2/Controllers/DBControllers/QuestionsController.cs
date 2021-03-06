﻿using System;
using System.Collections;
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
    [Authorize(Roles = "Examiner")]
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        public async Task<ActionResult> Index()
        {
            var questions = db.Questions.Include(q => q.RelatedSubject).Include(q => q.RelatedTopic);
            return View(await questions.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }



        // GET: Questions/Create
        [Authorize(Roles = "Examiner")]
        public ActionResult Create(string subject)
        {
            // limiting choices of Subject to those of Examiner
            string TeacherId = User.Identity.GetUserId();

          //  List<string> specificsubjects = new List<string>(from t in db.Teachings where t.ExaminerId == TeacherId select t.SubjectId);
            //IEnumerable<Subject> subjectlist = new List<Subject>(from s in db.Subjects where specificsubjects.Contains(s.SubjectId) select s);

            // limiting choices of Topics  to those of Examiner's Subjects 
            List<Topic> topics = new List<Topic>(from t in db.Topics where subject==t.SubjectId select t);

            IEnumerable<Subject> subjectchosen = new List<Subject>(from s in db.Subjects where s.SubjectId == subject select s);
            ViewBag.Subject = subject;
            ViewBag.SubjectId = new SelectList(subjectchosen, "SubjectId", "SubjectName");
            ViewBag.TopicId = new SelectList(topics, "TopicId", "TopicName");

            MultipleChoiceQuestion q = new MultipleChoiceQuestion();
            q.question.SubjectId = subject;
            
            return View(q);
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "QuestionId,SubjectId,TopicId,QuestionUsage,QuestionText,SampleAnswer,QuestionFormat")] Question question, [Bind(Include = "MultipleChoiceId,OptionText1,OptionText2,OptionText3,OptionText4,CorrectChoice,QuestionId")] MultipleChoice MQuestions)
        {
            if (question.SubjectId != null && question.TopicId != null && question.QuestionText != null && question.SampleAnswer != null) //instead of ModelState.isValid
            {
                bool acceptingMultiChoice = true;
           
                // if the question is a MultiChoice add the Multichoices else go back
                if (question.QuestionFormat == Question.QuestionType.MultipleChoice)
                {
                    //this used to carry out a redirection - BUT NO MORE!!!
                    //return RedirectToAction("Create", "MultipleChoices", new{ questionid = question.QuestionId});

                    if (MQuestions.OptionText1 != null && MQuestions.OptionText2 != null &&
                        MQuestions.OptionText3 != null && MQuestions.OptionText4 != null &&
                        MQuestions.CorrectChoice != 0)
                    {
                        MQuestions.QuestionId = question.QuestionId;
                        db.MultipleChoices.Add(MQuestions);
                    }
                    else
                    {
                        acceptingMultiChoice = false;
                        ViewBag.Error = "Incorrect Multiple Choice entry!!!";
                    }
                }

                if (acceptingMultiChoice)
                {
                    db.Questions.Add(question);
                    await db.SaveChangesAsync();

                    return RedirectToAction("EditQuestions", "Examiner", new {subject = question.SubjectId});
                }
            }
            else if (question.SampleAnswer == null)
            {
                ViewBag.Error = "Please enter a sample answer or choose an option";
            }
            else
            {
                ViewBag.Error = "Incorrect Question entry!!!";
            }


            // limiting choices of Subject to those of Examiner
            string TeacherId = User.Identity.GetUserId();
             

            List<Topic> topics = new List<Topic>(from t in db.Topics where question.SubjectId==t.SubjectId select t);

            ViewBag.Subject= question.SubjectId;
            ViewBag.SubjectId = question.SubjectId; // new SelectList(question.SubjectId, "SubjectId", "SubjectName", question.SubjectId);
            ViewBag.TopicId = new SelectList(topics, "TopicId", "TopicName", question.TopicId);

            MultipleChoiceQuestion q = new MultipleChoiceQuestion();
            q.question = question;
            q.MQuestions = MQuestions;
            return View(q);
        }

        // GET: Questions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", question.SubjectId);
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName", question.TopicId);

            MultipleChoiceQuestion q = new MultipleChoiceQuestion();
            q.question = question;
            if (question.QuestionFormat == Question.QuestionType.MultipleChoice)
            {
                q.MQuestions = db.MultipleChoices.Where(x => x.QuestionId == id).FirstOrDefault();
            }

            return View(q);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "QuestionId,SubjectId,TopicId,QuestionUsage,QuestionText,SampleAnswer,QuestionFormat")] Question question, [Bind(Include = "MultipleChoiceId,OptionText1,OptionText2,OptionText3,OptionText4,CorrectChoice,QuestionId")] MultipleChoice MQuestions)
        {
            if (question.SubjectId != null && question.TopicId != null && question.QuestionText != null && question.SampleAnswer != null) //instead of ModelState.IsValid
            {
                bool acceptingMultiChoice = true;

                //await db.SaveChangesAsync();

                if (question.QuestionFormat == Question.QuestionType.MultipleChoice)
                {
                    //int multiplechoiceid = (from m in db.MultipleChoices where m.QuestionId == question.QuestionId select m.MultipleChoiceId).FirstOrDefault();

                    //return RedirectToAction("Edit", "MultipleChoices", new { id = multiplechoiceid });

                    //editing the multiple choice questions
                    if (MQuestions.OptionText1 != null && MQuestions.OptionText2 != null &&
                        MQuestions.OptionText3 != null && MQuestions.OptionText4 != null &&
                        MQuestions.CorrectChoice != 0)
                    {
                        MQuestions.QuestionId = question.QuestionId;
                        MQuestions.MultipleChoiceId = db.MultipleChoices.AsNoTracking().Where(x => x.QuestionId == question.QuestionId).FirstOrDefault().MultipleChoiceId;
                        db.Entry(MQuestions).State = EntityState.Modified;
                    }
                    else
                    {
                        acceptingMultiChoice = false;
                        ViewBag.Error = "Incorrect Multiple Choice entry!!!";
                    }
                }

                if (acceptingMultiChoice)
                {
                    db.Entry(question).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    return RedirectToAction("EditQuestions", "Examiner", new { subject = question.SubjectId});
                }
            }
            else
            {
                ViewBag.Error = "Incorrect Question entry!!!";
            }

            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", question.SubjectId);
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName", question.TopicId);

            MultipleChoiceQuestion q = new MultipleChoiceQuestion();
            q.question = question;
            if (question.QuestionFormat == Question.QuestionType.MultipleChoice)
            {
                q.MQuestions = MQuestions;
            }

            return View(q);
        }

        // GET: Questions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Question question = await db.Questions.FindAsync(id);
            db.Questions.Remove(question);
            await db.SaveChangesAsync();
            return RedirectToAction("EditQuestions", "Examiner", new { subject = question.SubjectId });
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
