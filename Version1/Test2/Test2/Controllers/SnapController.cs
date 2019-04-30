using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test2.Models;
using Test2.Models.DBModels;
// using Microsoft.AspNet.Identity;

namespace Test2.Controllers.DBControllers
{
    public class SnapController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index(string examid)
        {
            ViewBag.ExamId = examid;
            Session["CapturedImage"] = "";
            return View();
        }


        [HttpPost]
        public ActionResult Index(string examid, string imagename)
        {
            ViewBag.ExamId = examid;
            string sss = Session["CapturedImage"].ToString();

            ViewBag.pic = "~/Captures" + Session["CapturedImage"].ToString();

            return View();
        }

        [HttpPost]
        public ActionResult Capture()
        {
            if (Request.InputStream.Length > 0)
            {
                using (StreamReader reader = new StreamReader(Request.InputStream))
                {
                    string hexString = Server.UrlEncode(reader.ReadToEnd());
                    string userName = User.Identity.Name; //System.Web.HttpContext.Current.User.Identity.Name;
                    // string timeNow = string.Format("{0}", DateTime.Now.ToString("dd-MM-yy hh-mm-ss"));
                    string imageName = string.Format("{0}_{1}", userName, DateTime.Now.ToString("dd-MM-yy"));
                    string imagePath = string.Format("~/Captures/{0}.png", imageName);
                    System.IO.File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString));
                    Session["CapturedImage"] = VirtualPathUtility.ToAbsolute(imagePath);
                    
                    using (var context = new ApplicationDbContext())
                    {

                        var UserImageDetails = new Shot
                        {
                            // UserID = User.Identity.GetUserId(),
                            
                            EnrollmentId = "SteStu19-TST2019",                                                 
                            ImageTitle = imageName,
                            ImageLocation = imagePath,
                            ShotTiming = DateTime.Now
                        };

                        context.Shots.Add(UserImageDetails);
                        context.SaveChanges();
                        reader.Close();
                        
                    }

                    

                }
            }
            return View();
        }

        [HttpPost]
        public ContentResult GetCapture()
        {
            string url = Session["CapturedImage"].ToString();
            Session["CapturedImage"] = null;
            return Content(url);
        }

        [HttpGet]
        public ActionResult LaunchSnap()
        {
            if (Convert.ToString(Session["CapturedImage"]) != string.Empty)
            {
                ViewBag.pic = "~/Captures" + Session["CapturedImage"].ToString();
              
            }
            else
            {
                ViewBag.pic = "/Content/person.jpg";
            }
            return View();
        }
        
        private static byte[] ConvertHexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }

}