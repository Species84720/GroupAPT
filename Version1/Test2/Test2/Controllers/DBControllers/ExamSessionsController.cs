﻿using System;
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
    public class ExamSessionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExamSessions
        public async Task<ActionResult> Index()
        {
            var examSessions = db.ExamSessions.Include(e => e.RelatedLocation).Include(e => e.RelatedSubject);
            return View(await examSessions.ToListAsync());
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
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Campus");
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");
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

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Campus", examSession.LocationId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", examSession.SubjectId);
            return View(examSession);
        }

        // GET: ExamSessions/Edit/5
        public async Task<ActionResult> Edit(string id)
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
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Campus", examSession.LocationId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", examSession.SubjectId);
            return View(examSession);
        }

        // POST: ExamSessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ExamId,SubjectId,LocationId,ExamDateTime,ExamEndTime,QuestionAmount,AccessCode,CodeIssueDateTime,FullyCorrected,MaxMark,MinMark,AvgMark,NumOfParticipants,NumOfFails")] ExamSession examSession)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examSession).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Campus", examSession.LocationId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", examSession.SubjectId);
            return View(examSession);
        }

        // GET: ExamSessions/Delete/5
        public async Task<ActionResult> Delete(string id)
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
