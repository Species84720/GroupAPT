using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Test2.Models;
using Test2.Models.DBModels;

namespace Test2.Controllers
{
    public class InvigilationController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invigilation
        public ActionResult Schedule()
        {
            string ingivilator = User.Identity.GetUserId();

            string name = (from u in db.Users where u.Id==ingivilator select u.FirstName+" "+u.Surname ).SingleOrDefault();
            
            
        //we get the list of invigilations that have not yet happened, related to this invigilation
            List<Invigilation> invigilations  =new List<Invigilation>(from i in db.Invigilations where i.UserId==ingivilator && i.RelatedExamSession.ExamDateTime>=DateTime.Now select i);


            ViewBag.Name = name;

            return View(invigilations);
        }


        public ActionResult MakeCode()
        {
            string ingivilator = User.Identity.GetUserId();

            string code="" ;

            ExamSession session = (from i in db.Invigilations where i.UserId == ingivilator && i.RelatedExamSession.ExamDateTime.Value.Day == DateTime.Now.Day && i.RelatedExamSession.ExamDateTime.Value.Minute <= DateTime.Now.Minute + 30 && i.RelatedExamSession.ExamDateTime.Value.Hour == DateTime.Now.Hour && i.RelatedExamSession.ExamDateTime.Value.Minute <= DateTime.Now.Minute + 30 select i.RelatedExamSession ).FirstOrDefault();

            //we get the list of invigilations that have not yet happened, related to this invigilation
            List<Invigilation> invigilations = new List<Invigilation>(from i in db.Invigilations where i.UserId == ingivilator && i.RelatedExamSession.ExamDateTime >= DateTime.Now select i);

            int num;

            if (session==null )
            {
                code = "There are no Active Exams: ";

                Random rand = new Random();

                for (int i = 0; i <= 5; i++)
                {
                    num = rand.Next(0, 35);
                    if (num < 10)
                    {
                        code = code + num;
                    }
                    else
                    {
                        code = code + (char)('A' + num - 9);
                    }

                }//end for

                ViewBag.Exam = "";

                ViewBag.Code = code;
                return View();
            }

            if (session.CodeIssueDateTime == null || session.CodeIssueDateTime.Value.Minute > DateTime.Now.Minute + 6)
            {
                Random rand = new Random();

                for (int i = 0; i <= 5; i++)
                {
                    num = rand.Next(0, 35);
                    if (num < 10)
                    {
                        code = code + num;
                    }
                    else
                    {
                        code = code + (char)('A' + num - 9);
                    }

                }//end for

                ExamSession toAlter = db.ExamSessions.Find(session.ExamId);

                if (toAlter != null) { //it shouldn't be null if session has a value

                    toAlter.AccessCode = code;
                toAlter.CodeIssueDateTime=DateTime.Now;
                db.Entry(toAlter).State = EntityState.Modified;
                db.SaveChanges();
                }

            }

            ViewBag.Exam = session.RelatedSubject.SubjectName;
            ViewBag.Code = code;


            return View();
        }



    }
}